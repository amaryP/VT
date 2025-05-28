import os

def save_ohlcv_csv(df, symbol, interval, exchange, start_date, end_date):
    """
    Sauvegarde le DataFrame OHLCV dans le dossier data/raw/crypto/BINANCE/ avec le nom conventionné.
    """
    symbol_clean = symbol.replace("/", "")
    dirpath = os.path.join(os.path.dirname(__file__), "..", "data", "raw", "crypto", exchange.upper())
    os.makedirs(dirpath, exist_ok=True)
    filename = f"{symbol_clean}_{interval}_{exchange.upper()}_{start_date}_{end_date}.csv"
    filepath = os.path.join(dirpath, filename)
    df.to_csv(filepath, index=False)
    print(f"[EXPORT] Données OHLCV exportées dans {filepath}")
