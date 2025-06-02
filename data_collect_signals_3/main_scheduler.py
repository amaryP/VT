import os
import subprocess
import time
from datetime import datetime
import glob

# Répertoires
BASE_DIR = os.path.dirname(os.path.abspath(__file__))
SRC_DIR = os.path.join(BASE_DIR, 'src')
DATA_DIR = os.path.join(BASE_DIR, 'DATA')
H252D_DIR = os.path.join(DATA_DIR, 'H252D')

# Fonctions utilitaires

def get_latest_file(pattern, directory):
    files = glob.glob(os.path.join(directory, pattern))
    if not files:
        return None
    return max(files, key=os.path.getmtime)

def ensure_h252d():
    today = datetime.now().strftime('%Y%m%d')
    h252d_files = glob.glob(os.path.join(H252D_DIR, f'highesthigh252d_{today}*.csv'))
    if not h252d_files:
        print('[INFO] Génération du fichier H252D...')
        subprocess.run(['python', os.path.join(SRC_DIR, 'gen_highesthigh252d.py')], check=True)
    else:
        print(f'[INFO] Fichier H252D déjà présent : {os.path.basename(h252d_files[-1])}')

def run_main_collect():
    print('[INFO] Lancement de la collecte principale...')
    subprocess.run(['python', os.path.join(SRC_DIR, 'main_collect.py')], check=True)

def run_analyse_signals():
    print('[INFO] Lancement de l\'analyse des signaux...')
    latest_csv = get_latest_file('nasdaq_1d_*.csv', DATA_DIR)
    if latest_csv:
        subprocess.run(['python', os.path.join(SRC_DIR, 'analyse_signals.py'), latest_csv], check=True)
    else:
        print('[ERREUR] Aucun fichier de collecte trouvé pour analyse.')

def run_collect_patterns():
    print('[INFO] Lancement de la détection de patterns...')
    latest_signals = get_latest_file('*_signals.csv', DATA_DIR)
    if latest_signals:
        subprocess.run(['python', os.path.join(SRC_DIR, 'collect_patterns.py'), latest_signals], check=True)
    else:
        print('[ERREUR] Aucun fichier _signals.csv trouvé pour détection de patterns.')

def pipeline():
    ensure_h252d()
    run_main_collect()
    run_analyse_signals()
    run_collect_patterns()

def wait_until(hour, minute):
    now = datetime.now()
    target = now.replace(hour=hour, minute=minute, second=0, microsecond=0)
    if now > target:
        target = target.replace(day=now.day + 1)
    wait_sec = (target - now).total_seconds()
    print(f"[INFO] Attente jusqu'à {target.strftime('%H:%M')} ({int(wait_sec)}s)...")
    time.sleep(max(0, wait_sec))

def main():
    print('[INFO] Lancement immédiat du pipeline pour vérification...')
    pipeline()
    while True:
        wait_until(22, 37)
        print('[INFO] Déclenchement du pipeline programmé à 22h37...')
        pipeline()

if __name__ == '__main__':
    main()
