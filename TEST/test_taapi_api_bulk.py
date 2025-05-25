import os
import unittest
from taapi_client import TaapiClient

class TestTaapiApiBulk(unittest.TestCase):
    def test_taapi_api_bulk(self):
        api_key = os.getenv("KEY_TAAPI_IO")
        test_name = "test_taapi_api_bulk"
        try:
            self.assertIsNotNone(api_key, "La variable d'environnement KEY_TAAPI_IO n'est pas définie.")
            taapi = TaapiClient()
            indicators = ["rsi", "macd", "bbands", "ema"]
            result = taapi.get_indicators_bulk("BTC/USDT", interval="1h", indicators=indicators)
            # Vérification de la présence des principaux indicateurs dans la réponse
            self.assertIn("rsi14", result, f"RSI manquant dans la réponse bulk: {result}")
            self.assertIn("macd", result, f"MACD manquant dans la réponse bulk: {result}")
            self.assertIn("bb_upper", result, f"Bollinger Bands manquant dans la réponse bulk: {result}")
            self.assertIn("ema", result, f"EMA manquant dans la réponse bulk: {result}")
            print(f"Réponse bulk: {result}")
        except Exception as e:
            self.fail(f"Exception lors du test bulk taapi.io: {e}")

if __name__ == "__main__":
    unittest.main()
