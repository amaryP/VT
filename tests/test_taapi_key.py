import os
from dotenv import load_dotenv

# Charge le fichier .env à la racine du projet
load_dotenv()

# Récupère la clé TAAPI.IO depuis l'environnement
api_key = os.getenv("KEY_TAAPI_IO")

if api_key:
    print(f"Clé TAAPI.IO trouvée : {api_key[:6]}... (tronquée)" )
else:
    print("Clé TAAPI.IO non trouvée dans l'environnement. Veuillez l'ajouter dans votre .env.")
