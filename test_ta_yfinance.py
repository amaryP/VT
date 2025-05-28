import yfinance as yf
import pandas as pd
from ta.momentum import RSIIndicator
from ta.trend import EMAIndicator
import sys

# Usage: python test_ta_yfinance.py <SYMBOL> <INTERVAL>
# Exemple: python test_ta_yfinance.py AAPL 15m

INTERVAL_MAPPING = ["1m", "2m", "5m", "15m", "30m", "60m", "90m", "1h", "1d", "5d", "1wk", "1mo", "3mo"]

def main(symbol, interval):
    if interval not in INTERVAL_MAPPING:
        print(f"[ERREUR] Intervalle non supporté: {interval}")
        print(f"Intervalle supportés: {INTERVAL_MAPPING}")
        sys.exit(1)

    print(f"[INFO] Téléchargement des données {symbol} intervalle {interval} depuis Yahoo Finance...")
    df = yf.download(tickers=symbol, period="7d", interval=interval)

    if df is None or df.empty:
        print("[ERREUR] Aucune donnée reçue depuis Yahoo Finance.")
        sys.exit(1)

    df = df.sort_index()
    df['rsi14'] = RSIIndicator(close=df['Close'], window=14).rsi()
    df['rsi5'] = RSIIndicator(close=df['Close'], window=5).rsi()
    df['ema20'] = EMAIndicator(close=df['Close'], window=20).ema_indicator()

    print("\n[RESULTATS - 10 dernières bougies] :")
    print(df[['Close', 'rsi14', 'rsi5', 'ema20']].tail(10))

if __name__ == "__main__":
    if len(sys.argv) != 3:
        print("Usage: python test_ta_yfinance.py <SYMBOL> <INTERVAL>")
        print("Exemple: python test_ta_yfinance.py AAPL 15m")
        sys.exit(1)
    symbol = sys.argv[1]
    interval = sys.argv[2]
    main(symbol, interval)
