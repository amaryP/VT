import os
import unittest
import psycopg2

# Utilitaire pour logguer le résultat d'un test dans la table testresult.resultats_tests
def log_test_result(test_name, status, details):
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
        print(f"Erreur lors du log du résultat de test : {e}")

class TestTaapiKeyEnv(unittest.TestCase):
    def test_taapi_key_env(self):
        test_name = "test_taapi_key_env"
        try:
            key = os.getenv("KEY_TAAPI_IO")
            self.assertIsNotNone(key, "La variable d'environnement KEY_TAAPI_IO n'est pas définie.")
            self.assertTrue(len(key) > 10, "La clé TAAPI.IO semble trop courte ou vide.")
            log_test_result(test_name, "OK", "Clé trouvée et longueur correcte.")
        except Exception as e:
            log_test_result(test_name, "KO", str(e))
            raise

if __name__ == "__main__":
    unittest.main()
