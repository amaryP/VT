import os
import logging
import requests
from datetime import datetime

def fetch_indicators(symbol, interval, api_key=None, debug=False):
    """
    Récupère les indicateurs EMA10, EMA21, ADX14, ATR14, Bollinger upper/middle/lower pour un actif donné via taapi.io (en un seul call bulk).
    Retourne un dict {champ: valeur} pour la dernière bougie close.
    Si debug=True, loggue la réponse brute et les erreurs éventuelles pour chaque indicateur.
    """
    logger = logging.getLogger("fetch_indicators")
    api_key = api_key or os.getenv("KEY_TAAPI_IO")
    if not api_key:
        logger.warning("Clé API TAAPI.IO manquante (KEY_TAAPI_IO)")
        raise RuntimeError("Clé API TAAPI.IO manquante (KEY_TAAPI_IO)")
    base_url = "https://api.taapi.io/bulk"
    # Préparation des indicateurs à récupérer
    indicators = [
        {"indicator": "ema", "optInTimePeriod": 10},
        {"indicator": "ema", "optInTimePeriod": 21},
        {"indicator": "adx", "optInTimePeriod": 14},
        {"indicator": "atr", "optInTimePeriod": 14},
        {"indicator": "bbands", "optInTimePeriod": 20, "stdDevUp": 2, "stdDevDn": 2},
        {"indicator": "candle"},
        {"indicator": "sma", "optInTimePeriod": 20, "field": "volume"},
        {"indicator": "rsi", "optInTimePeriod": 14}
    ]
    # Paramètres selon type d'actif
    if "/" not in symbol and ":" not in symbol:
        # Action US
        construct = {
            "type": "stocks",
            "symbol": symbol,
            "interval": interval,
            "indicators": indicators
        }
    else:
        # Crypto
        construct = {
            "exchange": "binance",
            "symbol": symbol,
            "interval": interval,
            "indicators": indicators
        }
    payload = {
        "secret": api_key,
        "construct": construct
    }
    try:
        logger.warning(f"[TAAPI] Récupération bulk pour {symbol}")
        resp = requests.post(base_url, json=payload)
        if debug:
            print(f"[DEBUG][{symbol}] HTTP status: {resp.status_code}")
        data = resp.json()
        if debug:
            logger.warning(f"[DEBUG][{symbol}] Réponse brute bulk : {data}")
            for entry in data.get("data", []):
                if entry.get("errors"):
                    logger.warning(f"[DEBUG][{symbol}] Erreurs pour {entry.get('id')}: {entry.get('errors')}")
        result = {}
        for entry in data.get("data", []):
            indicator = entry.get("indicator", "")
            res = entry.get("result", {})
            if indicator == "ema":
                # Cherche la période dans l'id (ex: 'stocks_AAPL_15m_ema_10_0')
                id_str = entry.get("id", "")
                period = None
                id_parts = id_str.split("_")
                for i, part in enumerate(id_parts):
                    if part == "ema" and i+1 < len(id_parts):
                        try:
                            period = int(id_parts[i+1])
                        except Exception:
                            period = None
                        break
                if period == 10:
                    result["ema_10"] = res.get("value")
                elif period == 21:
                    result["ema_21"] = res.get("value")
            elif indicator == "adx":
                result["adx_14"] = res.get("value")
            elif indicator == "atr":
                result["atr_14"] = res.get("value")
            elif indicator == "bbands":
                result["bollinger_upper"] = res.get("valueUpperBand")
                result["bollinger_middle"] = res.get("valueMiddleBand")
                result["bollinger_lower"] = res.get("valueLowerBand")
            elif indicator == "candle":
                result["open"] = res.get("open")
                result["close"] = res.get("close")
                result["volume"] = res.get("volume")
            elif indicator == "sma":
                # Pour SMA sur le volume
                if res.get("field") == "volume" or entry.get("id", "").endswith("sma_20_volume_0"):
                    result["sma_volume_20"] = res.get("value")
            elif indicator == "rsi":
                # On ne prend que la période 14 ici
                id_str = entry.get("id", "")
                if id_str.endswith("rsi_14_0"):
                    result["rsi_14"] = res.get("value")
        return result
    except Exception as e:
        logger.warning(f"Erreur lors de la récupération des indicateurs pour {symbol}: {e}")
        if debug:
            logger.warning(f"[DEBUG][{symbol}] Exception: {e}")
        raise
