import os
import psycopg2
from datetime import datetime
import unittest
import psycopg2.extras

class TestSignauxBrutsTable(unittest.TestCase):
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

    def test_insert_signaux_bruts(self):
        symbol = "TEST/UNIT"
        dateheure = datetime.now()
        rsi14 = 42.42
        rsi5 = 55.55
        bb_upper = 30000.0
        bb_lower = 25000.0
        bb_mid = 27500.0
        ema = 27000.0
        close = 27200.0
        open_ = 27100.0
        high = 27300.0
        low = 27000.0
        pattern = "hammer"
        eventlog = "test insertion signal brut"
        raw_json = {'test': 'ok'}
        intervalle = "1h"
        insert_query = """
        INSERT INTO signaux_bruts (symbol, dateheure, rsi14, rsi5, bb_upper, bb_lower, bb_mid, ema, close, open, high, low, pattern, eventlog, raw_json, intervalle)
        VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s)
        RETURNING id;
        """
        valeurs = (symbol, dateheure, rsi14, rsi5, bb_upper, bb_lower, bb_mid, ema, close, open_, high, low, pattern, eventlog, psycopg2.extras.Json(raw_json), intervalle)
        self.cur.execute(insert_query, valeurs)
        inserted_id = self.cur.fetchone()[0]
        self.conn.commit()
        self.assertIsNotNone(inserted_id)
        # Vérification lecture
        self.cur.execute("SELECT intervalle FROM signaux_bruts WHERE id = %s", (inserted_id,))
        row = self.cur.fetchone()
        self.assertIsNotNone(row)
        self.assertEqual(row[0], intervalle)
        # Nettoyage
        self.cur.execute("DELETE FROM signaux_bruts WHERE id = %s", (inserted_id,))
        self.conn.commit()

    def test_read_signaux_bruts(self):
        # On insère d'abord une ligne pour la lire ensuite
        symbol = "TEST/READ"
        dateheure = datetime.now()
        intervalle = "15m"
        insert_query = """
        INSERT INTO signaux_bruts (symbol, dateheure, intervalle)
        VALUES (%s, %s, %s)
        RETURNING id;
        """
        self.cur.execute(insert_query, (symbol, dateheure, intervalle))
        inserted_id = self.cur.fetchone()[0]
        self.conn.commit()
        # Lecture
        self.cur.execute("SELECT symbol, dateheure, intervalle FROM signaux_bruts WHERE id = %s", (inserted_id,))
        row = self.cur.fetchone()
        self.assertIsNotNone(row)
        self.assertEqual(row[0], symbol)
        self.assertEqual(row[2], intervalle)
        # Nettoyage
        self.cur.execute("DELETE FROM signaux_bruts WHERE id = %s", (inserted_id,))
        self.conn.commit()

if __name__ == "__main__":
    unittest.main()
