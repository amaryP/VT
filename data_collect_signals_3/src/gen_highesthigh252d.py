import os
import sys
import pandas as pd
from datetime import datetime
from pathlib import Path

try:
    import yfinance as yf
except ImportError:
    print("yfinance n'est pas installé. Faites: pip install yfinance")
    sys.exit(1)

# Base directory = racine du projet (2 niveaux au-dessus du script)
BASE_DIR = Path(__file__).resolve().parent.parent.parent
# Utilise la wishlist du pipeline principal
WISHLIST = BASE_DIR / 'data_collect_signals_3' / 'DATA' / 'wishlist.txt'
H252D_DIR = BASE_DIR / 'data_collect_signals_3' / 'DATA' / 'H252D'
today = datetime.now().strftime('%Y%m%dT%H%M%S')
OUT_CSV = H252D_DIR / f'highesthigh252d_{today}.csv'

# Crée le dossier si besoin
os.makedirs(H252D_DIR, exist_ok=True)

def get_symbols_from_wishlist(path):
    with open(path, 'r') as f:
        lines = f.readlines()
    # Ignore comments and empty lines
    return sorted(set([l.strip() for l in lines if l.strip() and not l.startswith('//')]))

def get_highesthigh(symbol):
    try:
        df = yf.download(symbol, period="1y", interval="1d", progress=False)
        if df.empty:
            return None
        val = df['High'].max()
        # Si val est un Series ou autre, tente de convertir en float
        try:
            val_float = float(val)
            return val_float
        except Exception:
            print(f"[WARN] Conversion impossible pour {symbol}: {val}")
            return None
    except Exception as e:
        print(f"Erreur pour {symbol}: {e}")
        return None

def main():
    symbols = get_symbols_from_wishlist(WISHLIST)
    results = []
    missing = []
    for sym in symbols:
        hh = get_highesthigh(sym)
        results.append({'actif': sym, 'highest_high_252d': hh})
        if hh is None or pd.isna(hh):
            missing.append(sym)
        print(f"{sym}: {hh}")
    pd.DataFrame(results).to_csv(OUT_CSV, index=False)
    if missing:
        print("\n[WARNING] Symboles sans données Yahoo (à vérifier ou exclure):")
        for m in missing:
            print(m)
        with open(H252D_DIR / f'highesthigh252d_{today}_missing.txt', 'w') as f:
            for m in missing:
                f.write(m + '\n')
    print(f"Fichier sauvegardé: {OUT_CSV}")

if __name__ == "__main__":
    main()
