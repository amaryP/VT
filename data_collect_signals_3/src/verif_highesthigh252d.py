import sys
import pandas as pd
from pathlib import Path

try:
    import yfinance as yf
except ImportError:
    print("yfinance n'est pas installé. Faites: pip install yfinance")
    sys.exit(1)

# Usage: python verif_highesthigh252d.py TICKER
if len(sys.argv) < 2:
    print("Usage: python verif_highesthigh252d.py TICKER")
    sys.exit(1)

ticker = sys.argv[1].upper()

# Fichier CSV généré par le script principal
from datetime import datetime
csv_path = Path(__file__).resolve().parent.parent / 'data_collect_signals_3' / 'DATA' / 'H252D' / f"highesthigh252d_{datetime.now().strftime('%Y%m%d')}.csv"

# Lecture valeur du CSV
try:
    df = pd.read_csv(csv_path)
    val_csv = df.loc[df['actif'] == ticker, 'highest_high_252d'].values[0]
    # Nettoyage si string multi-lignes
    if isinstance(val_csv, str) and ' ' in val_csv:
        val_csv = float(val_csv.split()[1])
    else:
        val_csv = float(val_csv)
except Exception as e:
    print(f"Erreur lecture CSV: {e}")
    sys.exit(1)

# Valeur calculée en direct
df_yf = yf.download(ticker, period="1y", interval="1d", progress=False)
if df_yf.empty:
    print(f"Pas de données yfinance pour {ticker}")
    sys.exit(1)
val_yf = df_yf['High'].max()

print(f"Ticker: {ticker}")
print(f"Valeur dans le CSV: {val_csv}")
print(f"Valeur calculée yfinance: {val_yf}")
print(f"Différence absolue: {abs(val_csv - val_yf)}")
