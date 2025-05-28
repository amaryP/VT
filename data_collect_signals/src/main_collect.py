import os
from datetime import datetime
from fetch_indicators import fetch_indicators
import csv
import logging
from datetime import timezone
import time
import requests

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
    interval = '15m'
    now = datetime.now(timezone.utc).replace(second=0, microsecond=0)
    # Nouveau nom de fichier unique pour tous les actifs
    filename = os.path.join(os.path.dirname(__file__), '..', 'DATA', f"nasdaq_{interval}_{now.strftime('%Y%m%dT%H%M')}.csv")
    with open(filename, 'w', newline='', encoding='utf-8') as fcsv:
        writer = csv.writer(fcsv)
        writer.writerow([
            'actif', 'datetime',
            'ema_10', 'ema_21', 'adx_14', 'atr_14',
            'bollinger_upper', 'bollinger_middle', 'bollinger_lower',
            'open', 'close', 'volume', 'sma_volume_20', 'rsi_14'
        ])
        total = len(symbols)
        for idx, symbol in enumerate(symbols, 1):
            try:
                print(f"[{idx}/{total}] Collecte {symbol}...")
                logging.warning(f"Début collecte pour {symbol}")
                indicators = call_taapi_with_retry(fetch_indicators, symbol, interval, debug=True)
                if indicators is None or not any(indicators.values()):
                    print(f"[{idx}/{total}] ATTENTION: Résultat vide pour {symbol}")
                writer.writerow([
                    symbol,
                    now.isoformat(),
                    indicators.get('ema_10') if indicators else None,
                    indicators.get('ema_21') if indicators else None,
                    indicators.get('adx_14') if indicators else None,
                    indicators.get('atr_14') if indicators else None,
                    indicators.get('bollinger_upper') if indicators else None,
                    indicators.get('bollinger_middle') if indicators else None,
                    indicators.get('bollinger_lower') if indicators else None,
                    indicators.get('open') if indicators else None,
                    indicators.get('close') if indicators else None,
                    indicators.get('volume') if indicators else None,
                    indicators.get('sma_volume_20') if indicators else None,
                    indicators.get('rsi_14') if indicators else None
                ])
                logging.warning(f"Ajout ligne CSV pour {symbol}")
            except Exception as e:
                print(f"[{idx}/{total}] Erreur pour {symbol}: {e}")
                logging.warning(f"Erreur pour {symbol} : {e}")
            time.sleep(0.5)  # Délai entre requêtes pour stabilité (modifié à 0.5s)

if __name__ == "__main__":
    main()
