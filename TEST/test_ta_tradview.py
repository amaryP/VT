from tvDatafeed import TvDatafeed, Interval
import pandas as pd
from ta.momentum import RSIIndicator
from ta.trend import EMAIndicator
import sys

INTERVAL_MAPPING = {
    "1m": Interval.in_1_minute,
    "5m": Interval.in_5_minute,
    "15m": Interval.in_15_minute,
    "1h": Interval.in_1_hour,
    "4h": Interval.in_4_hour,
    "1d": Interval.in_daily,
    "1W": Interval.in_weekly,
    "1M": Interval.in_monthly
}

def main(symbol, interval_str, exchange):
    if interval_str not in INTERVAL_MAPPING:
        print(f"[ERREUR] Intervalle non supporté: {interval_str}")
        sys.exit(1)

    print(f"[INFO] Connexion à TradingView...")
    tv = TvDatafeed()  # Connexion sans identifiants explicites si déjà authentifié

    print(f"[INFO] Récupération des données pour {symbol} sur {exchange} ({interval_str})...")
    df = tv.get_hist(symbol=symbol, exchange=exchange, interval=INTERVAL_MAPPING[interval_str], n_bars=100)

    if df is None or df.empty:
        print("[ERREUR] Aucune donnée reçue depuis TradingView.")
        sys.exit(1)

    df = df.sort_index()  # assure un tri chronologique

    # Calculs d'indicateurs
    df['rsi14'] = RSIIndicator(close=df['close'], window=14).rsi()
    df['rsi5'] = RSIIndicator(close=df['close'], window=5).rsi()
    df['ema20'] = EMAIndicator(close=df['close'], window=20).ema_indicator()

    # Affichage des 10 dernières lignes utiles
    print("\n[RESULTATS - 10 dernières bougies] :")
    print(df[['close', 'rsi14', 'rsi5', 'ema20']].tail(10))

if __name__ == "__main__":
    if len(sys.argv) != 4:
        print("Usage: python test_ta_tradview.py <SYMBOL> <INTERVAL> <EXCHANGE>")
        print("Exemple: python test_ta_tradview.py BTCUSDT 15m BINANCE")
        sys.exit(1)

    symbol = sys.argv[1]
    interval = sys.argv[2]
    exchange = sys.argv[3]
    main(symbol, interval, exchange)
