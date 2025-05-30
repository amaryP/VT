import time
from datetime import datetime, timedelta
import pytz
import subprocess
import os

# Heure de Paris
PARIS_TZ = pytz.timezone('Europe/Paris')
# Heure de lancement souhaitée (après clôture US, ex : 22h15)
TARGET_HOUR = 22
TARGET_MINUTE = 15

# Commande à exécuter pour la collecte (adapter si besoin)
COLLECT_CMD = [
    'python', os.path.join(os.path.dirname(__file__), 'main_collect.py')
]


def wait_until_target_time():
    while True:
        now = datetime.now(PARIS_TZ)
        target = now.replace(hour=TARGET_HOUR, minute=TARGET_MINUTE, second=0, microsecond=0)
        if now > target:
            # Si on a déjà dépassé l'heure cible aujourd'hui, attendre demain
            target += timedelta(days=1)
        wait_seconds = (target - now).total_seconds()
        print(f"[INFO] Prochaine collecte prévue à {target.strftime('%Y-%m-%d %H:%M:%S')} (dans {int(wait_seconds)}s)")
        if wait_seconds > 0:
            time.sleep(min(wait_seconds, 60))  # Réveille toutes les 60s pour vérifier
        else:
            break

def main():
    # Lancer une première collecte immédiatement pour test
    print(f"[INFO] Lancement immédiat de la collecte à {datetime.now(PARIS_TZ).strftime('%Y-%m-%d %H:%M:%S')}")
    try:
        subprocess.run(COLLECT_CMD, check=True)
    except Exception as e:
        print(f"[ERREUR] Echec de la collecte initiale : {e}")
    # Puis boucle quotidienne à l'heure cible
    while True:
        wait_until_target_time()
        print(f"[INFO] Lancement de la collecte à {datetime.now(PARIS_TZ).strftime('%Y-%m-%d %H:%M:%S')}")
        try:
            subprocess.run(COLLECT_CMD, check=True)
        except Exception as e:
            print(f"[ERREUR] Echec de la collecte : {e}")
        # Attendre 24h avant la prochaine collecte
        time.sleep(24 * 3600)

if __name__ == "__main__":
    main()
