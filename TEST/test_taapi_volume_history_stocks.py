# Ce script de test est déplacé dans le dossier TEST pour respecter l'organisation du projet.
# Il permet de tester la récupération de l'historique des volumes pour TSLA via taapi.io.
import os
import requests
import json

if __name__ == "__main__":
    API_KEY = os.getenv("KEY_TAAPI_IO")
    assert API_KEY, "La variable d'environnement KEY_TAAPI_IO doit être définie."

    symbol = "TSLA"
    interval = "1h"
    results = 20

    url = "https://api.taapi.io/bulk"
    payload = {
        "secret": API_KEY,
        "construct": {
            "type": "stocks",
            "symbol": symbol,
            "interval": interval,
            "indicators": [
                {"indicator": "volume", "results": results}
            ]
        }
    }
    headers = {"Content-Type": "application/json"}

    print("[INFO] Payload envoyé à taapi.io :")
    print(json.dumps(payload, indent=2))

    try:
        response = requests.post(url, json=payload, headers=headers, timeout=15)
        print(f"[INFO] Statut HTTP : {response.status_code}")
        data = response.json()
        print("[INFO] Réponse JSON :")
        print(json.dumps(data, indent=2))
        # Extraction brute des valeurs de volume si présentes
        for entry in data.get("data", []):
            if entry.get("indicator") == "volume":
                print("[RESULTAT] entry['result']:", entry.get("result"))
                if "values" in entry.get("result", {}):
                    print("[RESULTAT] Liste des volumes (values):", entry["result"]["values"])
                else:
                    print("[RESULTAT] Pas de clé 'values' dans le résultat volume.")
    except Exception as e:
        print("[ERREUR] Impossible de parser la réponse JSON ou timeout :", e)
