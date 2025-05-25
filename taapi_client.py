import os
import requests
import logging

class TaapiClient:
    def __init__(self, dry_run: bool = False):
        # Utilisation unique de KEY_TAAPI_IO
        self.api_key = os.getenv("KEY_TAAPI_IO")
        self.base_url = "https://api.taapi.io"
        self.dry_run = dry_run
        if not self.api_key and not self.dry_run:
            raise ValueError("KEY_TAAPI_IO non trouvée dans l'environnement")

    def get_indicators(self, symbol: str, interval: str = "1h"):
        # Appel désormais la méthode bulk pour compatibilité ascendante
        return self.get_indicators_bulk(symbol, interval, ["rsi", "macd", "bbands", "ema"])

    def get_indicators_bulk(self, symbol: str, interval: str = "1h", indicators: list = None):
        """
        Récupère plusieurs indicateurs en un seul appel bulk POST taapi.io.
        :param symbol: ex: 'BTC/USDT'
        :param interval: ex: '1h', '15m', ...
        :param indicators: liste d'indicateurs à récupérer (ex: ["rsi", "macd", "bbands", ...])
        :return: dict avec les valeurs extraites et le raw_json complet
        """
        if self.dry_run:
            logging.info(f"[DRY_RUN] Simulation bulk pour {symbol} {interval} {indicators}")
            # Exemple de données simulées
            return {
                "symbol": symbol,
                "interval": interval,
                "rsi14": 28.5,
                "macd": {"valueMACD": 1.2, "valueMACDSignal": 1.0, "valueMACDHist": 0.2},
                "bb_upper": 30000.0,
                "bb_lower": 25000.0,
                "bb_mid": 27500.0,
                "ema": 27000.0,
                "close": 27200.0,
                "open": 27100.0,
                "high": 27300.0,
                "low": 27000.0,
                "pattern": "hammer",
                "raw_json": {"simu": True}
            }
        if not indicators:
            indicators = ["rsi", "macd", "bbands", "ema"]
        payload = {
            "secret": self.api_key,
            "construct": {
                "exchange": "binance",
                "symbol": symbol,
                "interval": interval,
                "indicators": [{"indicator": ind} for ind in indicators]
            }
        }
        headers = {"Content-Type": "application/json"}
        response = requests.post(f"{self.base_url}/bulk", json=payload, headers=headers)
        if not response.ok:
            print(f"[TAAPI ERROR] HTTP {response.status_code}: {response.text}")
        response.raise_for_status()
        data = response.json()
        # Extraction des valeurs utiles pour la table signaux_bruts
        result = {"symbol": symbol, "interval": interval, "raw_json": data}
        for entry in data.get("data", []):
            ind_id = entry.get("id", "")
            res = entry.get("result", {})
            indicator = entry.get("indicator", "")
            # Mapping RSI par période
            if indicator == "rsi":
                # Cherche la période dans l'id (ex: ..._5_0 ou ..._14_0)
                parts = ind_id.split("_")
                if len(parts) >= 5:
                    try:
                        period = int(parts[4])
                        if period == 5:
                            result["rsi5"] = res.get("value")
                        elif period == 14:
                            result["rsi14"] = res.get("value")
                        else:
                            result[f"rsi{period}"] = res.get("value")
                    except Exception:
                        pass
                else:
                    # fallback: si pas de période trouvée, stocke comme rsi
                    result["rsi"] = res.get("value")
            elif indicator == "macd":
                result["macd"] = res
            elif indicator == "bbands" or "bb" in ind_id:
                result["bb_upper"] = res.get("valueUpperBand")
                result["bb_lower"] = res.get("valueLowerBand")
                result["bb_mid"] = res.get("valueMiddleBand")
            elif indicator == "ema":
                result["ema"] = res.get("value")
        return result
