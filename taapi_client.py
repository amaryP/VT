import os
import requests
import logging

class TaapiClient:
    def __init__(self, dry_run: bool = False, exchange: str = "binance"):
        # Utilisation unique de KEY_TAAPI_IO
        self.api_key = os.getenv("KEY_TAAPI_IO")
        self.base_url = "https://api.taapi.io"
        self.dry_run = dry_run
        self.exchange = exchange
        if not self.api_key and not self.dry_run:
            raise ValueError("KEY_TAAPI_IO non trouvée dans l'environnement")

    def get_indicators(self, symbol: str, interval: str = "1h"):
        # Appel désormais la méthode bulk pour compatibilité ascendante
        return self.get_indicators_bulk(symbol, interval, ["rsi", "macd", "bbands", "ema"])

    def get_indicators_bulk(self, symbol: str, interval: str = "1h", indicators: list = None, results: int = None):
        """
        Récupère plusieurs indicateurs en un seul appel bulk POST taapi.io.
        :param symbol: ex: 'BTC/USDT'
        :param interval: ex: '1h', '15m', ...
        :param indicators: liste d'indicateurs à récupérer (ex: ["rsi", "macd", "bbands", ...])
        :param results: nombre de résultats historiques à demander (pour l'historique)
        :return: dict avec les valeurs extraites et le raw_json complet
        """
        if self.dry_run:
            logging.info(f"[DRY_RUN] Simulation bulk pour {symbol} {interval} {indicators} results={results}")
            # Exemple de données simulées enrichies
            return {
                "symbol": symbol,
                "interval": interval,
                "rsi14": 28.5,
                "rsi5": 35.2,
                "ema20": 27000.0,
                "ema50": 26800.0,
                "bb_upper": 30000.0,
                "bb_lower": 25000.0,
                "bb_mid": 27500.0,
                "bb_width": 0.018,
                "volume": 1200.0,
                "volume_moy20": 1100.0,
                "volume_relatif": 1.09,
                "volume_relatif_moy6": 0.95,
                "macd_histogram": 0.2,
                "divergence_rsi": False,
                "pattern": "hammer",
                "context_spy": "bullish",
                "close": 27200.0,
                "open": 27100.0,
                "high": 27300.0,
                "low": 27000.0,
                "raw_json": {"simu": True}
            }
        if not indicators:
            indicators = [
                {"indicator": "rsi", "optInTimePeriod": 14},
                {"indicator": "rsi", "optInTimePeriod": 5},
                {"indicator": "ema", "optInTimePeriod": 20},
                {"indicator": "ema", "optInTimePeriod": 50},
                "macd", "bbands", "volume", "candle"
            ]
        # Correction stocks : type=stocks au lieu de exchange=stocks
        # Correction crypto : format du symbole pour Binance
        symbol_api = symbol
        construct = {
            "symbol": symbol_api,
            "interval": interval,
            "indicators": [ind if isinstance(ind, dict) else {"indicator": ind} for ind in indicators]
        }
        if self.exchange == "stocks":
            construct["type"] = "stocks"
        else:
            construct["exchange"] = self.exchange
        payload = {
            "secret": self.api_key,
            "construct": construct
        }
        if results is not None:
            payload["results"] = results
        headers = {"Content-Type": "application/json"}
        print(f"[TAAPI DEBUG] Payload envoyé: {payload}")
        response = requests.post(f"{self.base_url}/bulk", json=payload, headers=headers)
        if not response.ok:
            print(f"[TAAPI ERROR] HTTP {response.status_code}: {response.text}")
        try:
            response.raise_for_status()
        except Exception as e:
            # Si erreur 504 ou autre, et qu'on est en binance, tente une fois avec symbol sans slash
            if self.exchange == "binance" and "/" in symbol:
                symbol_api2 = symbol.replace("/", "")
                print(f"[TAAPI DEBUG] Retry avec symbol='{symbol_api2}' (format sans slash)")
                construct["symbol"] = symbol_api2
                payload["construct"] = construct
                print(f"[TAAPI DEBUG] Payload retry: {payload}")
                response2 = requests.post(f"{self.base_url}/bulk", json=payload, headers=headers)
                if not response2.ok:
                    print(f"[TAAPI ERROR] HTTP {response2.status_code}: {response2.text}")
                response2.raise_for_status()
                data = response2.json()
            else:
                raise
        else:
            data = response.json()
        if not data or not data.get("data"):
            print(f"[TAAPI ERROR] Réponse vide ou incohérente pour symbol={symbol_api} (exchange={self.exchange})")
        # Extraction enrichie des valeurs utiles pour la table signaux_bruts
        result = {"symbol": symbol, "interval": interval, "raw_json": data}
        ema20 = ema50 = volume = volume_moy20 = None
        for entry in data.get("data", []):
            ind_id = entry.get("id", "")
            res = entry.get("result", {})
            indicator = entry.get("indicator", "")
            # RSI
            if indicator == "rsi":
                period = None
                parts = ind_id.split("_")
                if len(parts) >= 5:
                    try:
                        period = int(parts[4])
                    except Exception:
                        pass
                if period == 5:
                    result["rsi5"] = res.get("value")
                elif period == 14:
                    result["rsi14"] = res.get("value")
                else:
                    result[f"rsi{period}"] = res.get("value")
            elif indicator == "macd":
                result["macd_histogram"] = res.get("valueMACDHist")
            elif indicator == "bbands" or "bb" in ind_id:
                result["bb_upper"] = res.get("valueUpperBand")
                result["bb_lower"] = res.get("valueLowerBand")
                result["bb_mid"] = res.get("valueMiddleBand")
                # bb_width = (upper-lower)/mid
                try:
                    upper = res.get("valueUpperBand")
                    lower = res.get("valueLowerBand")
                    mid = res.get("valueMiddleBand")
                    if upper is not None and lower is not None and mid:
                        result["bb_width"] = (upper - lower) / mid if mid else None
                except Exception:
                    pass
            elif indicator == "ema":
                period = None
                parts = ind_id.split("_")
                if len(parts) >= 5:
                    try:
                        period = int(parts[4])
                    except Exception:
                        pass
                if period == 20:
                    result["ema20"] = res.get("value")
                elif period == 50:
                    result["ema50"] = res.get("value")
            elif indicator == "volume":
                result["volume"] = res.get("value")
            elif indicator == "candle":
                result["open"] = res.get("open")
                result["high"] = res.get("high")
                result["low"] = res.get("low")
                result["close"] = res.get("close")
                result["volume"] = res.get("volume")
            elif indicator in [
                "invertedhammer", "engulfing", "hammer", "hangingman", "doji", "morningstar", "eveningstar", "shootingstar", "piercing", "harami", "haramicross", "belthold", "longline", "shortline", "highwave", "stalledpattern", "breakaway", "tasukigap", "sticksandwich", "ladderbottom", "kickingbylength"
            ]:
                val = res.get("value")
                detected = val in (100, -100, "100", "-100")
                col = f"pattern_{indicator.lower()}"
                result[col] = detected
        # Calculs dérivés (ex: volume_moy20, volume_relatif, etc.)
        # À compléter selon logique métier (ex: stocker historique pour calculs glissants)
        return result
