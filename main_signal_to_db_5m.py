import os
from datetime import datetime
from taapi_client import TaapiClient
from pg_logger import PgLogger
import json
from calcul_indicateurs import volume_moyenne, volume_relatif, volume_relatif_moyenne, detect_divergence_rsi

def log_integration_result(test_name, status, details):
    try:
        pc_id = "PC1000"
        import psycopg2
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

def make_json_safe(obj):
    # Rend récursivement un objet JSON-serializable (datetime, date, etc.)
    if isinstance(obj, dict):
        return {k: make_json_safe(v) for k, v in obj.items()}
    elif isinstance(obj, list):
        return [make_json_safe(v) for v in obj]
    elif hasattr(obj, 'isoformat'):
        return obj.isoformat()
    else:
        return obj

def load_wishlist_symbols():
    """
    Charge la wishlist selon la variable d'environnement VT_WISHLIST_TYPE (crypto/usstock).
    Par défaut : crypto.
    """
    wl_type = os.getenv("VT_WISHLIST_TYPE", "crypto").lower()
    if wl_type == "usstock":
        filepath = os.path.join(os.path.dirname(__file__), "wishlist_symbols_usstock.txt")
    else:
        filepath = os.path.join(os.path.dirname(__file__), "wishlist_symbols.txt")
    symbols = []
    try:
        with open(filepath, "r", encoding="utf-8") as f:
            for line in f:
                line = line.strip()
                if line and not line.startswith("#") and not line.startswith("//"):
                    symbols.append(line.split()[0])
    except Exception as e:
        print(f"[ERREUR] Impossible de lire la wishlist {filepath} : {e}")
    return symbols

if __name__ == "__main__":
    try:
        # Détection du type d'asset pour l'exchange
        wl_type = os.getenv("VT_WISHLIST_TYPE", "crypto").lower()
        if wl_type == "usstock":
            exchange = "stocks"
        else:
            exchange = "binance"
        intervalle = "5m"
        eventlog = "Scan batch wishlist intégration signaux bruts"
        indicateurs_bulk = ["rsi", "macd", "bbands", "ema", {"indicator": "rsi", "optInTimePeriod": 5}]
        taapi = TaapiClient(dry_run=os.getenv("DRY_RUN", "0") == "1", exchange=exchange)
        logger = PgLogger()
        symbols = load_wishlist_symbols()
        for symbol in symbols:
            indicateurs = taapi.get_indicators_bulk(symbol, interval=intervalle)
            volumes_hist = []
            closes_hist = []
            rsis_hist = []
            try:
                indicators_hist = [
                    {"indicator": "volume", "results": 20},
                    {"indicator": "candle", "results": 20},
                    {"indicator": "rsi", "optInTimePeriod": 14, "results": 20}
                ]
                hist = taapi.get_indicators_bulk(symbol, interval=intervalle, indicators=indicators_hist)
                print(f"[DEBUG] {symbol} hist raw_json: {hist.get('raw_json')}")
                for entry in hist.get("raw_json", {}).get("data", []):
                    indicator = entry.get("indicator")
                    result = entry.get("result", {})
                    if indicator == "volume":
                        values = result.get("values")
                        if values is None and isinstance(result.get("value"), list):
                            values = result["value"]
                        print(f"[DEBUG] {symbol} volume values: {values}")
                        if values:
                            volumes_hist.extend([v for v in values if v is not None])
                    elif indicator == "candle":
                        values = result.get("values")
                        print(f"[DEBUG] {symbol} candle values: {values}")
                        if values:
                            closes_hist.extend([v.get("close") for v in values if v.get("close") is not None])
                    elif indicator == "rsi":
                        values = result.get("values")
                        print(f"[DEBUG] {symbol} rsi values: {values}")
                        if values:
                            rsis_hist.extend([v for v in values if v is not None])
            except Exception as e:
                print(f"[DEBUG] Exception lors de la récupération de l'historique pour {symbol}: {e}")
            print(f"[DEBUG] {symbol} volumes_hist: {volumes_hist}")
            print(f"[DEBUG] {symbol} closes_hist: {closes_hist}")
            print(f"[DEBUG] {symbol} rsis_hist: {rsis_hist}")
            volume_moy20 = volume_moyenne(volumes_hist)
            volume = indicateurs.get("volume")
            volume_relatif_val = volume_relatif(volume, volume_moy20)
            # Calcul du volume_relatif_moy6 (moyenne glissante sur 6 derniers volume_relatif)
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
                "pattern": indicateurs.get("pattern"),
                "eventlog": eventlog,
                "raw_json": make_json_safe(indicateurs),
                "valeur": indicateurs.get("close"),
                "intervalle": intervalle,
                "volume": volume,
                "volume_moy20": volume_moy20,
                "volume_relatif": volume_relatif_val,
                "volume_relatif_moy6": volume_relatif_moy6,
                "macd_histogram": indicateurs.get("macd_histogram"),
                "divergence_rsi": divergence_rsi_val,
                "context_spy": None,
                "strategy_match": None
            }
            inserted_id = logger.log_signal_brut(signal)
            print(f"Signal brut inséré avec id={inserted_id} (intervalle={intervalle}, symbol={symbol})")
        logger.close()
        log_integration_result(f"integration_signal_to_db_{intervalle}", "OK", f"{len(symbols)} signaux insérés intervalle={intervalle}")
    except Exception as e:
        print(f"[FATAL] Erreur inattendue dans le script : {e}")
