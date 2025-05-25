import os
from datetime import datetime
from taapi_client import TaapiClient
from pg_logger import PgLogger
import psycopg2
import copy
import json
from indicators_rules import evaluate_rsi, evaluate_macd, evaluate_bbands

# Fonction utilitaire pour logguer le résultat d'intégration
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
    # Log fichier
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
    elif isinstance(obj, (datetime,)):
        return obj.isoformat()
    else:
        return obj

def load_wishlist_symbols(filepath="wishlist_symbols.txt"):
    symbols = []
    try:
        with open(filepath, "r", encoding="utf-8") as f:
            for line in f:
                line = line.strip()
                if line and not line.startswith("#"):
                    symbols.append(line)
    except Exception as e:
        print(f"[ERREUR] Impossible de lire la wishlist : {e}")
    return symbols

if __name__ == "__main__":
    # Paramètres de test
    intervalle = "1h"
    eventlog = "Scan batch wishlist intégration signaux bruts"
    indicateurs_bulk = ["rsi", "macd", "bbands", "ema", {"indicator": "rsi", "optInTimePeriod": 5}]
    taapi = TaapiClient(dry_run=os.getenv("DRY_RUN", "0") == "1")
    logger = PgLogger()
    symbols = load_wishlist_symbols()
    for symbol in symbols:
        indicateurs = taapi.get_indicators_bulk(symbol, interval=intervalle, indicators=indicateurs_bulk)
        rsi5 = None
        for entry in indicateurs.get("raw_json", {}).get("data", []):
            if entry.get("indicator") == "rsi" and entry.get("id", "").endswith("_5_0"):
                rsi5 = entry.get("result", {}).get("value")
        # Application des règles
        rsi_signal = evaluate_rsi(indicateurs.get("rsi14"))
        macd_val = None
        macd_signal_val = None
        if isinstance(indicateurs.get("macd"), dict):
            macd_val = indicateurs["macd"].get("valueMACD")
            macd_signal_val = indicateurs["macd"].get("valueMACDSignal")
        macd_signal = evaluate_macd(macd_val, macd_signal_val)
        bbands_signal = evaluate_bbands(indicateurs.get("close"), indicateurs.get("bb_upper"), indicateurs.get("bb_lower"))
        signal = {
            "symbol": symbol,
            "dateheure": datetime.now(),
            "rsi14": indicateurs.get("rsi14"),
            "macd": indicateurs.get("macd"),
            "bb_upper": indicateurs.get("bb_upper"),
            "bb_lower": indicateurs.get("bb_lower"),
            "bb_mid": indicateurs.get("bb_mid"),
            "ema": indicateurs.get("ema"),
            "close": indicateurs.get("close"),
            "open": indicateurs.get("open"),
            "high": indicateurs.get("high"),
            "low": indicateurs.get("low"),
            "pattern": indicateurs.get("pattern"),
            "eventlog": eventlog,
            "raw_json": make_json_safe(indicateurs),
            "valeur": indicateurs.get("close"),
            "intervalle": intervalle,
            "rsi5": rsi5,
            "rsi_signal": rsi_signal,
            "macd_signal": macd_signal,
            "bbands_signal": bbands_signal
        }
        try:
            print(f"[DEBUG] {symbol} raw_json before insert:", signal["raw_json"])
            json.dumps(signal["raw_json"])
        except Exception as e:
            print(f"[DEBUG] Erreur de sérialisation JSON pour {symbol} :", e)
            continue
        test_name = f"integration_signal_to_db_1h_{symbol.replace('/', '_')}"
        try:
            inserted_id = logger.log_signal_brut(signal)
            print(f"Signal brut inséré avec id={inserted_id} (intervalle={intervalle}, symbol={symbol})")
            log_integration_result(test_name, "OK", f"Signal inséré id={inserted_id} intervalle={intervalle} symbol={symbol}")
        except Exception as e:
            log_integration_result(test_name, "KO", str(e))
            print(f"[ERREUR] Insertion échouée pour {symbol} : {e}")
    logger.close()
