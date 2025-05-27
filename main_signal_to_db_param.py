import os
import sys
from datetime import datetime
from taapi_client import TaapiClient
import psycopg2
from calcul_indicateurs import volume_moyenne, volume_relatif, volume_relatif_moyenne, detect_divergence_rsi
from context_spy import get_context_spy
import time
import requests

def make_json_safe(obj):
    if isinstance(obj, dict):
        return {k: make_json_safe(v) for k, v in obj.items()}
    elif isinstance(obj, list):
        return [make_json_safe(v) for v in obj]
    elif hasattr(obj, 'isoformat'):
        return obj.isoformat()
    else:
        return obj

def load_wishlist_symbols(crypto=False):
    wl_type = os.getenv("VT_WISHLIST_TYPE", "crypto").lower()
    print(f"[DEBUG] load_wishlist_symbols: wl_type={wl_type}, crypto={crypto}")
    if crypto:
        filepath = os.path.join(os.path.dirname(__file__), "wishlist_symbols_crypto.txt")
    elif wl_type == "usstock":
        filepath = os.path.join(os.path.dirname(__file__), "wishlist_symbols_usstock.txt")
    else:
        filepath = os.path.join(os.path.dirname(__file__), "wishlist_symbols.txt")
    print(f"[DEBUG] load_wishlist_symbols: filepath={filepath}")
    symbols = []
    try:
        with open(filepath, "r", encoding="utf-8") as f:
            for line in f:
                line = line.strip()
                if line and not line.startswith("#") and not line.startswith("//"):
                    symbols.append(line.split()[0])
        print(f"[DEBUG] load_wishlist_symbols: {len(symbols)} symbols loaded. Extrait: {symbols[:5]}")
        if not symbols:
            print(f"[WARNING] Wishlist vide: {filepath}")
    except Exception as e:
        print(f"[ERREUR] Impossible de lire la wishlist {filepath} : {e}")
    return symbols

def call_taapi_with_retry(taapi_method, *args, max_retries=3, sleep_seconds=15, **kwargs):
    import time
    import requests
    for attempt in range(1, max_retries + 1):
        try:
            return taapi_method(*args, **kwargs)
        except Exception as e:
            status_code = None
            if hasattr(e, 'response') and getattr(e.response, 'status_code', None):
                status_code = e.response.status_code
            elif isinstance(e, requests.exceptions.HTTPError) and getattr(e.response, 'status_code', None):
                status_code = e.response.status_code
            # Gestion rate-limit 429
            if status_code == 429 or '429' in str(e):
                print(f"[TAAPI RATE-LIMIT] 429 reçu, tentative {attempt}/{max_retries}. Attente {sleep_seconds}s...")
                time.sleep(sleep_seconds)
            # Gestion erreurs serveur 5xx (dont 504)
            elif status_code is not None and 500 <= status_code < 600:
                print(f"[TAAPI ERROR] {status_code} reçu (erreur serveur), tentative {attempt}/{max_retries}. Attente {sleep_seconds}s...")
                time.sleep(sleep_seconds)
            elif '504' in str(e):
                print(f"[TAAPI ERROR] 504 Gateway Timeout (texte), tentative {attempt}/{max_retries}. Attente {sleep_seconds}s...")
                time.sleep(sleep_seconds)
            else:
                raise
    print(f"[TAAPI ERROR] Erreur persistante après {max_retries} essais. Passage au symbole suivant.")
    return None

def log_integration_result(test_name, status, details):
    try:
        pc_id = "PC1000"
        conn = psycopg2.connect(
            host=os.getenv(f"BDD_VT_PG_HOST_{pc_id}"),
            port=os.getenv(f"BDD_VT_PG_PORT_{pc_id}"),
            dbname=os.getenv(f"BDD_VT_PG_DB_{pc_id}"),
            user=os.getenv(f"BDD_VT_PG_USER_VT_{pc_id}"),
            password=os.getenv(f"BDD_VT_PG_USER_VT_PASSWORD_{pc_id}")
        )
        cur = conn.cursor()
        cur.execute(
            """
            INSERT INTO testresult.resultats_tests (test_name, status, details)
            VALUES (%s, %s, %s)
            """,
            (test_name, status, details)
        )
        conn.commit()
        cur.close()
        conn.close()
    except Exception as e:
        print(f"Erreur lors du log du résultat d'intégration : {e}")
    try:
        with open("TEST/LOGS/test_results.log", "a", encoding="utf-8") as f:
            f.write(f"{datetime.now().isoformat()} | {test_name} | {status} | {details}\n")
    except Exception as e:
        print(f"Erreur lors du log fichier : {e}")

def main_signal(intervalle, crypto=False):
    # Import ici pour casser la boucle
    from pg_logger import PgLogger
    try:
        if crypto:
            exchange = "binance"
            context_spy_value = ''
        else:
            exchange = "stocks"
            context_spy_value = get_context_spy(intervalle)
        eventlog = f"Scan batch intégration signaux bruts (intervalle={intervalle})"
        indicateurs_bulk = ["rsi", "macd", "bbands", "ema", {"indicator": "rsi", "optInTimePeriod": 5}]
        taapi = TaapiClient(dry_run=os.getenv("DRY_RUN", "0") == "1", exchange=exchange)
        logger = PgLogger()
        symbols = load_wishlist_symbols(crypto=crypto)
        print(f"[INFO] {len(symbols)} symboles à traiter : {symbols if len(symbols) < 30 else symbols[:30] + ['...']} ")
        if not symbols:
            print("[ERREUR] La wishlist est vide, arrêt du script.")
            exit(1)
        for symbol in symbols:
            indicateurs = call_taapi_with_retry(taapi.get_indicators_bulk, symbol, interval=intervalle)
            if indicateurs is None:
                print(f"[WARN] Impossible de récupérer les indicateurs pour {symbol} (rate-limit). Skip.")
                continue
            volumes_hist = []
            closes_hist = []
            rsis_hist = []
            try:
                indicators_hist = [
                    {"indicator": "volume", "results": 20},
                    {"indicator": "candle", "results": 20},
                    {"indicator": "rsi", "optInTimePeriod": 14, "results": 20}
                ]
                hist = call_taapi_with_retry(taapi.get_indicators_bulk, symbol, interval=intervalle, indicators=indicators_hist)
                if hist is None:
                    print(f"[WARN] Impossible de récupérer l'historique pour {symbol} (rate-limit). Skip.")
                    continue
                for entry in hist.get("raw_json", {}).get("data", []):
                    indicator = entry.get("indicator")
                    result = entry.get("result", {})
                    if indicator == "volume":
                        values = result.get("values")
                        if values is None and isinstance(result.get("value"), list):
                            values = result["value"]
                        if values:
                            volumes_hist.extend([v for v in values if v is not None])
                    elif indicator == "candle":
                        values = result.get("values")
                        if values:
                            closes_hist.extend([v.get("close") for v in values if v.get("close") is not None])
                        elif result.get("close") and isinstance(result.get("close"), list):
                            closes_hist.extend([v for v in result["close"] if v is not None])
                    elif indicator == "rsi":
                        values = result.get("values")
                        if values:
                            rsis_hist.extend([v for v in values if v is not None])
                        elif result.get("value") and isinstance(result.get("value"), list):
                            rsis_hist.extend([v for v in result["value"] if v is not None])
            except Exception as e:
                print(f"[DEBUG] Exception lors de la récupération de l'historique pour {symbol}: {e}")
                continue
            if not volumes_hist or not closes_hist or not rsis_hist:
                print(f"[WARN] Données historiques manquantes pour {symbol}. Skip.")
                continue
            volume_moy20 = volume_moyenne(volumes_hist)
            volume = indicateurs.get("volume")
            volume_relatif_val = volume_relatif(volume, volume_moy20)
            volume_relatifs_hist = []
            for i in range(len(volumes_hist)):
                v = volumes_hist[i]
                moy = volume_moyenne(volumes_hist[max(0, i-19):i+1])
                if moy:
                    volume_relatifs_hist.append(volume_relatif(v, moy))
                else:
                    volume_relatifs_hist.append(None)
            volume_relatif_moy6 = volume_relatif_moyenne([v for v in volume_relatifs_hist if v is not None], n=6)
            divergence_rsi_val = detect_divergence_rsi(closes_hist, rsis_hist)
            all_pattern_indicators = [
                {"indicator": "invertedhammer"},
                {"indicator": "engulfing"},
                {"indicator": "eveningstar"},
                {"indicator": "doji"},
                {"indicator": "spinningtop"}
            ]
            pattern_bool = {}
            pattern_found = False
            for i in range(0, len(all_pattern_indicators), 20):
                chunk = all_pattern_indicators[i:i+20]
                patterns_result = call_taapi_with_retry(taapi.get_indicators_bulk, symbol, interval=intervalle, indicators=chunk)
                if patterns_result is None:
                    print(f"[WARN] Impossible de récupérer les patterns pour {symbol} (rate-limit). Skip patterns.")
                    continue
                for entry in patterns_result.get("raw_json", {}).get("data", []):
                    indicator = entry.get("indicator", "")
                    res = entry.get("result", {})
                    val = res.get("value")
                    if indicator == "invertedhammer":
                        pattern_bool["pattern_inverted_hammer"] = val in (100, -100, "100", "-100")
                    elif indicator == "engulfing":
                        pattern_bool["pattern_bullish_engulfing"] = val in (100, "100")
                        pattern_bool["pattern_bearish_engulfing"] = val in (-100, "-100")
                    elif indicator == "eveningstar":
                        pattern_bool["pattern_evening_star"] = val in (100, -100, "100", "-100")
                    elif indicator == "doji":
                        pattern_bool["pattern_doji"] = val in (100, -100, "100", "-100")
                    elif indicator == "spinningtop":
                        pattern_bool["pattern_spinning_top"] = val in (100, -100, "100", "-100")
                    if val in (100, -100, "100", "-100"):
                        pattern_found = True
            signal = {
                "symbol": symbol,
                "dateheure": datetime.now(),
                "rsi14": indicateurs.get("rsi14"),
                "rsi5": indicateurs.get("rsi5"),
                "ema20": indicateurs.get("ema20"),
                "ema50": indicateurs.get("ema50"),
                "ema": indicateurs.get("ema20"),
                "bb_upper": indicateurs.get("bb_upper"),
                "bb_lower": indicateurs.get("bb_lower"),
                "bb_mid": indicateurs.get("bb_mid"),
                "bb_width": indicateurs.get("bb_width"),
                "close": indicateurs.get("close"),
                "open": indicateurs.get("open"),
                "high": indicateurs.get("high"),
                "low": indicateurs.get("low"),
                "pattern": "CALL",
                "eventlog": eventlog,
                "raw_json": make_json_safe(indicateurs),
                "valeur": indicateurs.get("close"),
                "intervalle": intervalle,
                "volume": volume,
                "volume_moy20": volume_moyenne(volumes_hist),
                "volume_relatif": volume_relatif_val,
                "volume_relatif_moy6": volume_relatif_moyenne([v for v in volume_relatifs_hist if v is not None], n=6),
                "macd_histogram": indicateurs.get("macd_histogram"),
                "divergence_rsi": divergence_rsi_val,
                "context_spy": context_spy_value
            }
            signal.update(pattern_bool)
            inserted_id = logger.log_signal_brut(signal)
            print(f"Signal brut inséré avec id={inserted_id} (intervalle={intervalle}, symbol={symbol})")
        logger.close()
        log_integration_result(f"integration_signal_to_db_{intervalle}", "OK", f"{len(symbols)} signaux insérés intervalle={intervalle}")
    except Exception as e:
        print(f"[FATAL] Erreur inattendue dans le script : {e}")

if __name__ == "__main__":
    if len(sys.argv) not in (2, 3):
        print("Usage: python main_signal_to_db_param.py <intervalle> [crypto]")
        print("Exemple: python main_signal_to_db_param.py 15m crypto")
        sys.exit(1)
    intervalle = sys.argv[1]
    crypto = False
    if len(sys.argv) == 3 and sys.argv[2].lower() == "crypto":
        crypto = True
    main_signal(intervalle, crypto)
