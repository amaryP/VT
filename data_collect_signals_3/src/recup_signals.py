import os
import logging
import requests
from datetime import datetime

def fetch_indicators(symbol, interval, api_key=None, debug=False):
    """
    Récupère les indicateurs nécessaires à la stratégie filtered_pattern_breakout via taapi.io (en un seul call bulk).
    Retourne un dict {champ: valeur} pour la dernière bougie close.
    """
    logger = logging.getLogger("fetch_indicators")
    api_key = api_key or os.getenv("KEY_TAAPI_IO")
    if not api_key:
        logger.warning("Clé API TAAPI.IO manquante (KEY_TAAPI_IO)")
        raise RuntimeError("Clé API TAAPI.IO manquante (KEY_TAAPI_IO)")
    base_url = "https://api.taapi.io/bulk"
    # Indicateurs nécessaires
    indicators = [
        {"indicator": "ema", "optInTimePeriod": 50},
        {"indicator": "ema", "optInTimePeriod": 100},
        {"indicator": "ema", "optInTimePeriod": 200},
        {"indicator": "candle"},
        {"indicator": "sma", "optInTimePeriod": 4, "field": "volume"}
        # highesthigh désactivé car non supporté sur actions
        # {"indicator": "highesthigh", "optInTimePeriod": 252}
    ]
    construct = {
        "type": "stocks",
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
                if period == 50:
                    result["ema_50"] = res.get("value")
                elif period == 100:
                    result["ema_100"] = res.get("value")
                elif period == 200:
                    result["ema_200"] = res.get("value")
            elif indicator == "candle":
                result["close_price"] = res.get("close")
                result["volume_today"] = res.get("volume")
            elif indicator == "sma":
                # SMA sur le volume (4 jours)
                if res.get("field") == "volume" or entry.get("id", "").endswith("sma_4_volume_0"):
                    result["volume_avg_4d"] = res.get("value")
            # highesthigh désactivé
            # elif indicator == "highesthigh":
            #     result["highest_high_252d"] = res.get("value")
        return result
    except Exception as e:
        logger.warning(f"Erreur lors de la récupération des indicateurs pour {symbol}: {e}")
        if debug:
            logger.warning(f"[DEBUG][{symbol}] Exception: {e}")
        raise

def fetch_patterns(symbol, interval, patterns, api_key=None, debug=False):
    """
    Appelle TAAPI.IO pour détecter un ou plusieurs patterns chartistes sur un symbole donné.
    Retourne le nom du pattern détecté (ou None si aucun).
    """
    logger = logging.getLogger("fetch_patterns")
    api_key = api_key or os.getenv("KEY_TAAPI_IO")
    if not api_key:
        logger.warning("Clé API TAAPI.IO manquante (KEY_TAAPI_IO)")
        raise RuntimeError("Clé API TAAPI.IO manquante (KEY_TAAPI_IO)")
    base_url = "https://api.taapi.io/bulk"
    indicators = [{"indicator": p} for p in patterns]
    construct = {
        "type": "stocks",
        "symbol": symbol,
        "interval": interval,
        "indicators": indicators
    }
    payload = {
        "secret": api_key,
        "construct": construct
    }
    try:
        logger.warning(f"[TAAPI] Détection patterns pour {symbol}")
        resp = requests.post(base_url, json=payload)
        if debug:
            print(f"[DEBUG][{symbol}] HTTP status: {resp.status_code}")
        data = resp.json()
        if debug:
            logger.warning(f"[DEBUG][{symbol}] Réponse brute bulk : {data}")
        for entry in data.get("data", []):
            indicator = entry.get("indicator", "")
            res = entry.get("result", {})
            if res.get("value") == 100:
                return indicator  # Retourne le nom du pattern détecté
        return None
    except Exception as e:
        logger.warning(f"Erreur lors de la détection de patterns pour {symbol}: {e}")
        if debug:
            logger.warning(f"[DEBUG][{symbol}] Exception: {e}")
        return None
