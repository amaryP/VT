import os
import psycopg2
import unittest
import json
from datetime import datetime

class TestInsertSignauxTriggers(unittest.TestCase):
    def setUp(self):
        pc_id = "PC1000"
        self.conn = psycopg2.connect(
            host=os.getenv(f"BDD_VT_PG_HOST_{pc_id}"),
            port=os.getenv(f"BDD_VT_PG_PORT_{pc_id}"),
            dbname=os.getenv(f"BDD_VT_PG_DB_{pc_id}"),
            user=os.getenv(f"BDD_VT_PG_USER_VT_{pc_id}"),
            password=os.getenv(f"BDD_VT_PG_USER_VT_PASSWORD_{pc_id}")
        )
        self.cur = self.conn.cursor()

    def tearDown(self):
        self.cur.close()
        self.conn.close()

    def log_test_result(self, test_name, status, details):
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

    def test_insert_signaux_triggers(self):
        # Données de test
        symbol = "BTC/USDT"
        intervalle = "1h"
        signal_code = "signal_achat_strong"
        signal_description = "Achat prioritaire (RSI14<30, RSI5>RSI14, close<BBLower)"
        rsi14 = 25.0
        rsi5 = 35.0
        macd = 0.5
        macd_signal = 0.3
        close = 25000.0
        bb_upper = 26000.0
        bb_lower = 24500.0
        bb_mid = 25250.0
        raw_json = json.dumps({"test": True})
        inserted_id = None
        try:
            self.cur.execute("""
                INSERT INTO signaux_triggers (dateheure, symbol, intervalle, signal_code, signal_description, rsi14, rsi5, macd, macd_signal, close, bb_upper, bb_lower, bb_mid, raw_json)
                VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s)
                RETURNING id
            """, (datetime.now(), symbol, intervalle, signal_code, signal_description, rsi14, rsi5, macd, macd_signal, close, bb_upper, bb_lower, bb_mid, raw_json))
            inserted_id = self.cur.fetchone()[0]
            self.conn.commit()
            self.cur.execute("SELECT symbol, signal_code FROM signaux_triggers WHERE id = %s", (inserted_id,))
            row = self.cur.fetchone()
            self.assertEqual(row[0], symbol)
            self.assertEqual(row[1], signal_code)
            self.cur.execute("DELETE FROM signaux_triggers WHERE id = %s", (inserted_id,))
            self.conn.commit()
            self.log_test_result("test_insert_signaux_triggers", "OK", f"Signal inséré id={inserted_id} symbol={symbol}")
        except Exception as e:
            if inserted_id:
                self.cur.execute("DELETE FROM signaux_triggers WHERE id = %s", (inserted_id,))
                self.conn.commit()
            self.log_test_result("test_insert_signaux_triggers", "KO", str(e))
            raise

if __name__ == "__main__":
    unittest.main()
