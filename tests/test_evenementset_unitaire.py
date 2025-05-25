import os
import psycopg2
from datetime import datetime
import unittest
import logging

# Configuration du logger pour écrire les résultats dans un fichier log
logging.basicConfig(
    filename=os.path.join(os.path.dirname(__file__), 'LOGS', 'test_results.log'),
    level=logging.INFO,
    format='%(asctime)s - %(levelname)s - %(message)s'
)

# Utilitaire pour logguer le résultat d'un test dans la table testresult.resultats_tests

def log_test_result(cur, test_name, status, details):
    # Log dans la base
    try:
        cur.execute(
            """
            INSERT INTO testresult.resultats_tests (test_name, status, details)
            VALUES (%s, %s, %s)
            """,
            (test_name, status, details)
        )
        cur.connection.commit()
    except Exception as e:
        print(f"Erreur lors du log du résultat de test : {e}")
    # Log dans le fichier
    log_msg = f"{test_name} | {status} | {details}"
    if status == "OK":
        logging.info(log_msg)
    else:
        logging.error(log_msg)

class TestEvenementsetDB(unittest.TestCase):
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

    def test_connexion(self):
        test_name = "test_connexion"
        try:
            self.cur.execute("SELECT 1;")
            result = self.cur.fetchone()
            self.assertEqual(result[0], 1)
            log_test_result(self.cur, test_name, "OK", "Connexion réussie.")
        except Exception as e:
            log_test_result(self.cur, test_name, "KO", str(e))
            self.fail(f"Connexion à la base échouée : {e}")

    def test_insert_evenementset(self):
        test_name = "test_insert_evenementset"
        symbol = "TEST/UNIT"
        dateheure = datetime.now()
        valeur = 12345.67
        rsi14 = 50.00
        rsi5 = 55.00
        figure = "test_figure"
        eventlog = "test unitaire insertion"
        details = ""
        inserted_row = None
        try:
            insert_query = """
            INSERT INTO evenementset (symbol, dateheure, valeur, rsi14, rsi5, figure, eventlog)
            VALUES (%s, %s, %s, %s, %s, %s, %s)
            RETURNING id, symbol, dateheure, valeur, rsi14, rsi5, figure, eventlog;
            """
            valeurs = (symbol, dateheure, valeur, rsi14, rsi5, figure, eventlog)
            self.cur.execute(insert_query, valeurs)
            inserted_row = self.cur.fetchone()
            self.conn.commit()
            self.assertIsNotNone(inserted_row)
            self.assertEqual(inserted_row[1], symbol)
            self.assertAlmostEqual(float(inserted_row[3]), valeur, places=5)
            self.assertAlmostEqual(float(inserted_row[4]), rsi14, places=2)
            self.assertAlmostEqual(float(inserted_row[5]), rsi5, places=2)
            self.assertEqual(inserted_row[6], figure)
            self.assertEqual(inserted_row[7], eventlog)
            details = f"Ligne insérée id={inserted_row[0]}"
            log_test_result(self.cur, test_name, "OK", details)
        except Exception as e:
            details = str(e)
            log_test_result(self.cur, test_name, "KO", details)
            self.conn.rollback()
            self.fail(f"Insertion échouée : {e}")
        finally:
            # Nettoyage uniquement si l'insertion a réussi
            if inserted_row is not None:
                try:
                    self.cur.execute("DELETE FROM evenementset WHERE id = %s", (inserted_row[0],))
                    self.conn.commit()
                except Exception as cleanup_e:
                    print(f"Erreur lors du nettoyage : {cleanup_e}")

if __name__ == "__main__":
    # Exécution automatique des tests unitaires à chaque lancement du programme
    print("\n===== Exécution automatique de la batterie de tests unitaires =====")
    runner = unittest.TextTestRunner(verbosity=2, resultclass=unittest.TextTestResult)
    suite = unittest.defaultTestLoader.loadTestsFromTestCase(TestEvenementsetDB)
    result = runner.run(suite)

    total = result.testsRun
    failed = len(result.failures) + len(result.errors)
    passed = total - failed
    percent = (passed / total * 100) if total > 0 else 0

    if failed > 0:
        print(f"\n\033[91mAttention : {failed} test(s) en échec sur {total} ({percent:.1f}% de réussite)\033[0m")
    else:
        print(f"\n\033[92mTous les tests sont passés avec succès ({percent:.1f}%)\033[0m")
