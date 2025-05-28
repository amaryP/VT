import os
from taapi_fetcher import fetch_ohlcv
from file_manager import save_ohlcv_csv
from datetime import datetime, timedelta

def main():
    # Pour une action US (ex: Tesla sur NASDAQ via taapi.io)
    symbol = "TSLA"  # Pour les actions, pas de /USDT
    interval = "1d"
    exchange = "nasdaq"  # d'après la doc taapi.io
    # Période : 7 jours glissants
    dt_end = datetime.utcnow()
    dt_start = dt_end - timedelta(days=7)
    start_date = dt_start.strftime("%Y-%m-%d")
    end_date = dt_end.strftime("%Y-%m-%d")
    df = fetch_ohlcv(symbol, interval, exchange, start_date, end_date)
    save_ohlcv_csv(df, symbol, interval, exchange, start_date, end_date)

if __name__ == "__main__":
    main()
