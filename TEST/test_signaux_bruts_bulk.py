import os
import unittest
from datetime import datetime
from taapi_client import TaapiClient
from pg_logger import PgLogger
import psycopg2
from main_signal_to_db import make_json_safe

def make_json_safe(obj):
    # Rend récursivement un objet JSON-serializable (datetime, date, etc.)
    import datetime
    if isinstance(obj, dict):
        return {k: make_json_safe(v) for k, v in obj.items()}
    elif isinstance(obj, list):
        return [make_json_safe(v) for v in obj]
    elif isinstance(obj, (datetime.datetime, datetime.date)):
        return obj.isoformat()
    else:
        return obj

class TestSignauxBrutsBulk(unittest.TestCase):
    def test_bulk_insert_signaux_bruts(self):
        api_key = os.getenv("KEY_TAAPI_IO")
        self.assertIsNotNone(api_key, "La variable d'environnement KEY_TAAPI_IO n'est pas définie.")
        symbol = "BTC/USDT"
        intervalle = "1h"
        eventlog = "test bulk insert signaux_bruts"
        indicateurs_bulk = [
            "rsi", "macd", "bbands", "ema", "doji", "engulfing", "3whitesoldiers"
        ]
        taapi = TaapiClient()
        indicateurs = taapi.get_indicators_bulk(symbol, interval=intervalle, indicators=indicateurs_bulk)
        print("Réponse API bulk:", indicateurs)
        signal = {
            "symbol": symbol,
            "dateheure": datetime.now().isoformat(),
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
            "intervalle": intervalle
        }
        logger = PgLogger()
        inserted_id = logger.log_signal_brut(signal)
        self.assertIsNotNone(inserted_id, "L'insertion du signal brut a échoué (id None)")
        # Vérification lecture en base
        conn = logger.conn
        cur = conn.cursor()
        cur.execute("SELECT symbol, intervalle, rsi14, bb_upper, ema, raw_json FROM signaux_bruts WHERE id = %s", (inserted_id,))
        row = cur.fetchone()
        self.assertIsNotNone(row, "Aucune ligne trouvée pour l'id inséré")
        self.assertEqual(row[0], symbol)
        self.assertEqual(row[1], intervalle)
        # Vérifie la présence de la clé 'macd' dans le raw_json
        import json
        raw_json = row[5]
        if isinstance(raw_json, str):
            raw_json = json.loads(raw_json)
        self.assertIn("macd", raw_json, "La clé 'macd' n'est pas présente dans raw_json")
        # Nettoyage (optionnel)
        cur.execute("DELETE FROM signaux_bruts WHERE id = %s", (inserted_id,))
        conn.commit()
        cur.close()
        logger.close()

if __name__ == "__main__":
    unittest.main()
