import time
import datetime
import pytz
import os
import sys

# Pour import dynamique des modules collecte/analyse
SRC_DIR = os.path.dirname(__file__)
sys.path.append(SRC_DIR)

# Fuseau horaire Paris
PARIS_TZ = pytz.timezone('Europe/Paris')

# Heure de fin (22h00 Paris)
END_HOUR = 22

# Fonction pour attendre le prochain créneau h+1, h+16, h+31, h+46

def wait_next_slot():
    now = datetime.datetime.now(PARIS_TZ)
    minute = now.minute
    second = now.second
    # Prochains slots : 1, 16, 31, 46
    next_slots = [1, 16, 31, 46]
    next_slot = min([m for m in next_slots if m > minute] + [next_slots[0] + 60])
    if next_slot > 59:
        # Prochain créneau à l'heure suivante
        next_time = now.replace(minute=1, second=0, microsecond=0) + datetime.timedelta(hours=1)
    else:
        next_time = now.replace(minute=next_slot, second=0, microsecond=0)
    wait_sec = (next_time - now).total_seconds()
    print(f"[INFO] Prochain lancement à {next_time.strftime('%H:%M:%S')} (dans {int(wait_sec)}s)")
    time.sleep(max(0, wait_sec))

def main_loop():
    import importlib.util
    # Import dynamique du script d'analyse (qui lance aussi la collecte)
    analyse_path = os.path.join(SRC_DIR, 'analyse_signals.py')
    spec = importlib.util.spec_from_file_location("analyse_signals", analyse_path)
    analyse_signals = importlib.util.module_from_spec(spec)
    spec.loader.exec_module(analyse_signals)
    while True:
        now = datetime.datetime.now(PARIS_TZ)
        if now.hour >= END_HOUR:
            print(f"[INFO] Arrêt automatique : il est {now.strftime('%H:%M')} à Paris (>=22h)")
            break
        print(f"[INFO] Lancement collecte+analyse à {now.strftime('%H:%M:%S')} (heure Paris)")
        try:
            analyse_signals.main()
        except Exception as e:
            print(f"[ERREUR] Exception lors de l'exécution : {e}")
        wait_next_slot()

if __name__ == "__main__":
    main_loop()
