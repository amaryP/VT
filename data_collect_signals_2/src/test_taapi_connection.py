# Test de connexion à TAAPI.IO
import os
import requests

api_key = os.environ.get("KEY_TAAPI_IO")
if not api_key:
    print("[ERREUR] La variable d'environnement KEY_TAAPI_IO n'est pas définie.")
    exit(1)

url = "https://api.taapi.io/rsi"
params = {
    "secret": api_key,
    "exchange": "binance",
    "symbol": "BTC/USDT",
    "interval": "1d"
}

try:
    resp = requests.get(url, params=params)
    data = resp.json()
    if resp.status_code == 200 and "value" in data:
        print(f"[SUCCÈS] Connexion TAAPI.IO OK, RSI: {data['value']}")
    else:
        print(f"[ERREUR] Réponse inattendue: {data}")
except Exception as e:
    print(f"[ERREUR] Exception lors de la connexion à TAAPI.IO: {e}")
