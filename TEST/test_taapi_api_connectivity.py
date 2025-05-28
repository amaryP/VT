import os
import requests
import unittest
import psycopg2
import logging

# Configuration du logger pour écrire les résultats dans un fichier log
logging.basicConfig(
    filename=os.path.join(os.path.dirname(__file__), 'LOGS', 'test_results.log'),
    level=logging.INFO,
    format='%(asctime)s - %(levelname)s - %(message)s'
)

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
    # Log dans le fichier
    log_msg = f"{test_name} | {status} | {details}"
    if status == "OK":
        logging.info(log_msg)
    else:
        logging.error(log_msg)

class TestTaapiApiKey(unittest.TestCase):
    """
    Test unitaire pour vérifier la validité de la clé API taapi.io et la connexion à l'API.
    Logue le résultat en base et dans le fichier de log via la structure existante.
    """
    def test_taapi_api_key_validity(self):
        api_key = os.getenv("KEY_TAAPI_IO")
        test_name = "test_taapi_api_key_validity"
        try:
            self.assertIsNotNone(api_key, "La variable d'environnement KEY_TAAPI_IO n'est pas définie.")
            url = f"https://api.taapi.io/rsi?secret={api_key}&exchange=binance&symbol=BTC/USDT&interval=1h"
            response = requests.get(url)
            self.assertEqual(response.status_code, 200, f"Erreur API taapi.io : {response.status_code} {response.text}")
            data = response.json()
            self.assertIn("value", data, f"Réponse inattendue de l'API : {data}")
            log_test_result(test_name, "OK", "Connexion API OK")
        except Exception as e:
            log_test_result(test_name, "KO", str(e))
            raise

if __name__ == "__main__":
    unittest.main()
