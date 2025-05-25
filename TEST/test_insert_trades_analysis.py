import os
import psycopg2
import unittest
import json
from datetime import datetime, timedelta

class TestInsertTradesAnalysis(unittest.TestCase):
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

    def test_insert_trades_analysis(self):
        # Données de test
        date_ouverture = datetime.now() - timedelta(hours=1)
        date_fermeture = datetime.now()
        symbol = "BTC/USDT"
        intervalle = "1h"
        type_signal = "signal_achat_strong"
        prix_ouverture = 25000.0
        prix_fermeture = 25500.0
        quantite = 0.1
        pnl = 50.0
        duree_trade = date_fermeture - date_ouverture
        statut = "CLOSED"
        details = json.dumps({"test": True})
        # Insertion
        self.cur.execute("""
            INSERT INTO trades_analysis (date_ouverture, date_fermeture, symbol, intervalle, type_signal, prix_ouverture, prix_fermeture, quantite, pnl, duree_trade, statut, details)
            VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s)
            RETURNING id
        """, (date_ouverture, date_fermeture, symbol, intervalle, type_signal, prix_ouverture, prix_fermeture, quantite, pnl, duree_trade, statut, details))
        inserted_id = self.cur.fetchone()[0]
        self.conn.commit()
        # Vérification
        self.cur.execute("SELECT symbol, type_signal, statut FROM trades_analysis WHERE id = %s", (inserted_id,))
        row = self.cur.fetchone()
        self.assertEqual(row[0], symbol)
        self.assertEqual(row[1], type_signal)
        self.assertEqual(row[2], statut)
        # Nettoyage
        self.cur.execute("DELETE FROM trades_analysis WHERE id = %s", (inserted_id,))
        self.conn.commit()
        self.log_test_result("test_insert_trades_analysis", "OK", f"Trade inséré id={inserted_id} symbol={symbol}")
        inserted_id = None
        try:
            # ...existing code...
            self.cur.execute("DELETE FROM trades_analysis WHERE id = %s", (inserted_id,))
            self.conn.commit()
            self.log_test_result("test_insert_trades_analysis", "OK", f"Trade inséré id={inserted_id} symbol={symbol}")
        except Exception as e:
            if inserted_id:
                self.cur.execute("DELETE FROM trades_analysis WHERE id = %s", (inserted_id,))
                self.conn.commit()
            self.log_test_result("test_insert_trades_analysis", "KO", str(e))
            raise

if __name__ == "__main__":
    unittest.main()
