import os
from datetime import datetime
from recup_signals import fetch_indicators
import csv
import logging
from datetime import timezone
import time
import requests
import pandas as pd
import subprocess
import re

def call_taapi_with_retry(taapi_method, *args, max_retries=3, sleep_seconds=10, **kwargs):
    """
    Wrapper pour gérer le rate-limit taapi.io (erreur 429) avec retry/sleep et log du code HTTP.
    """
    for attempt in range(1, max_retries + 1):
        try:
            return taapi_method(*args, **kwargs)
        except Exception as e:
            status_code = None
            if hasattr(e, 'response') and getattr(e.response, 'status_code', None):
                status_code = e.response.status_code
            elif isinstance(e, requests.exceptions.HTTPError) and getattr(e.response, 'status_code', None):
                status_code = e.response.status_code
            if status_code == 429 or '429' in str(e):
                print(f"[TAAPI RATE-LIMIT] 429 reçu, tentative {attempt}/{max_retries}. Attente {sleep_seconds}s...")
                time.sleep(sleep_seconds)
            elif status_code is not None and 500 <= status_code < 600:
                print(f"[TAAPI ERROR] {status_code} reçu (erreur serveur), tentative {attempt}/{max_retries}. Attente {sleep_seconds}s...")
                time.sleep(sleep_seconds)
            else:
                print(f"[TAAPI ERROR] Exception non gérée: {e}")
                raise
    print(f"[TAAPI ERROR] Erreur persistante après {max_retries} essais. Passage au symbole suivant.")
    return None

def main():
    logging.basicConfig(filename=os.path.join(os.path.dirname(__file__), '..', 'logs', 'collect.log'), level=logging.WARNING, format='%(asctime)s %(levelname)s %(message)s', encoding='utf-8')
    # Lecture de la wishlist (uniquement actions)
    wishlist_path = os.path.join(os.path.dirname(__file__), '..', 'DATA', 'wishlist.txt')
    with open(wishlist_path, 'r', encoding='utf-8') as f:
        symbols = [line.strip() for line in f if line.strip() and not line.startswith('#') and '/' not in line]

    # Lecture ou génération du fichier highesthigh252d du jour
    today = datetime.now().strftime('%Y%m%d')
    h252d_path = os.path.join(os.path.dirname(__file__), '..', 'DATA', 'H252D', f'highesthigh252d_{today}.csv')
    # Recherche du fichier highesthigh252d du jour le plus récent (si plusieurs)
    h252d_dir = os.path.join(os.path.dirname(__file__), '..', 'DATA', 'H252D')
    pattern = f'highesthigh252d_{today}.csv'
    h252d_files = [f for f in os.listdir(h252d_dir) if f.startswith(f'highesthigh252d_{today}') and f.endswith('.csv')]
    if h252d_files:
        # Trie par date de modification, prend le plus récent
        h252d_files = sorted(h252d_files, key=lambda f: os.path.getmtime(os.path.join(h252d_dir, f)), reverse=True)
        h252d_path = os.path.join(h252d_dir, h252d_files[0])
        print(f"[INFO] Fichier highesthigh252d utilisé : {h252d_files[0]}")
    else:
        h252d_path = os.path.join(h252d_dir, pattern)
        print(f"[INFO] Aucun fichier highesthigh252d trouvé pour aujourd'hui, génération en cours...")
        script_path = os.path.join(os.path.dirname(__file__), 'gen_highesthigh252d.py')
        result = subprocess.run(['python', script_path], capture_output=True, text=True)
        if result.returncode != 0:
            print(f"[ERREUR] Impossible de générer le fichier highesthigh252d :\n{result.stderr}")
            return
        print(f"[INFO] Fichier highesthigh252d généré.")

    # Lecture du fichier highesthigh252d du jour
    df_hh = pd.read_csv(h252d_path)
    # Nettoyage si besoin (float uniquement)
    def parse_hh(val):
        if isinstance(val, str):
            # Cherche le premier float dans la chaîne
            match = re.search(r"([0-9]+\.[0-9]+)", val)
            if match:
                return float(match.group(1))
            return None
        try:
            return float(val)
        except Exception:
            return None
    df_hh['highest_high_252d'] = df_hh['highest_high_252d'].apply(parse_hh)
    hh_dict = dict(zip(df_hh['actif'], df_hh['highest_high_252d']))

    interval = '1d'  # Swing trading : intervalle daily
    now = datetime.now(timezone.utc).replace(second=0, microsecond=0)
    # Nouveau nom de fichier unique pour tous les actifs
    filename = os.path.join(os.path.dirname(__file__), '..', 'DATA', f"nasdaq_{interval}_{now.strftime('%Y%m%dT%H%M')}.csv")
    with open(filename, 'w', newline='', encoding='utf-8') as fcsv:
        writer = csv.writer(fcsv)
        writer.writerow([
            'actif', 'datetime',
            'ema_50', 'ema_100', 'ema_200',
            'close_price', 'volume_today', 'volume_avg_4d', 'highest_high_252d'
        ])
        total = len(symbols)
        for idx, symbol in enumerate(symbols, 1):
            try:
                print(f"[{idx}/{total}] Collecte {symbol}...")
                logging.warning(f"Début collecte pour {symbol}")
                indicators = call_taapi_with_retry(fetch_indicators, symbol, interval, debug=True)
                if indicators is None or not any(indicators.values()):
                    print(f"[{idx}/{total}] ATTENTION: Résultat vide pour {symbol}")
                # On remplit highest_high_252d depuis le fichier H252D
                highest_high_252d = hh_dict.get(symbol)
                writer.writerow([
                    symbol,
                    now.isoformat(),
                    indicators.get('ema_50') if indicators else None,
                    indicators.get('ema_100') if indicators else None,
                    indicators.get('ema_200') if indicators else None,
                    indicators.get('close_price') if indicators else None,
                    indicators.get('volume_today') if indicators else None,
                    indicators.get('volume_avg_4d') if indicators else None,
                    highest_high_252d
                ])
                logging.warning(f"Ajout ligne CSV pour {symbol}")
            except Exception as e:
                print(f"[{idx}/{total}] Erreur pour {symbol}: {e}")
                logging.warning(f"Erreur pour {symbol} : {e}")
            time.sleep(0.5)  # Délai entre requêtes pour stabilité (modifié à 0.5s)

if __name__ == "__main__":
    main()
