import yfinance as yf
import pandas as pd
from ta.momentum import RSIIndicator
from ta.trend import EMAIndicator
import sys
import os

# Usage: python test_ta_yfinance.py <SYMBOL> <INTERVAL>
# Exemple: python test_ta_yfinance.py AAPL 15m

INTERVAL_MAPPING = ["1m", "2m", "5m", "15m", "30m", "60m", "90m", "1h", "1d", "5d", "1wk", "1mo", "3mo"]

def main(symbol, interval, crypto=False):
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
    # --- Ajout : ignorer la dernière bougie si elle est en cours (date du jour) ---
    import datetime
    today = datetime.datetime.utcnow().date()
    if df.tail(1).index[0].date() == today:
        print("[INFO] Dernière bougie incomplète détectée, elle sera ignorée pour le calcul des indicateurs.")
        df = df.iloc[:-1]
    # --- Fin ajout ---
    close = df['Close']
    if isinstance(close, pd.DataFrame):
        close = close.iloc[:, 0]
    close = close.astype(float)
    df['rsi14'] = RSIIndicator(close=close, window=14).rsi()
    df['rsi5'] = RSIIndicator(close=close, window=5).rsi()
    df['ema20'] = EMAIndicator(close=close, window=20).ema_indicator()

    print("\n[RESULTATS - 10 dernières bougies] :")
    print(df[['Close', 'rsi14', 'rsi5', 'ema20']].tail(10))

    # Extraction de la dernière bougie et conversion des valeurs
    def extract_float(val):
        if hasattr(val, 'values'):
            return float(val.values[-1])
        try:
            return float(val)
        except Exception:
            return None

    last_idx = df.tail(1).index[0]
    last = df.loc[last_idx]
    log_dict = {
        'symbol': symbol,
        'dateheure': str(last_idx),
        'rsi14': extract_float(last.get('rsi14')),
        'rsi5': extract_float(last.get('rsi5')),
        'bb_upper': None,
        'bb_lower': None,
        'bb_mid': None,
        'bb_width': None,
        'ema': extract_float(last.get('ema20')),
        'ema20': extract_float(last.get('ema20')),
        'ema50': None,
        'close': extract_float(last.get('Close')),
        'open': extract_float(last.get('Open')),
        'high': extract_float(last.get('High')),
        'low': extract_float(last.get('Low')),
        'pattern': '',
        'eventlog': 'test yfinance',
        'raw_json': '{}',
        'valeur': extract_float(last.get('Close')),
        'intervalle': interval,
        'volume': extract_float(last.get('Volume')),
        'volume_moy20': None,
        'volume_relatif': None,
        'volume_relatif_moy6': None,
        'macd_histogram': None,
        'divergence_rsi': None,
        'context_spy': '',
        'strategy_match': ''
    }
    # Ecriture dans un fichier log format table signaux_bruts
    log_dir = os.path.join(os.path.dirname(__file__), 'LOGS')
    os.makedirs(log_dir, exist_ok=True)
    log_path = os.path.join(log_dir, f"yfinance_{symbol}_{interval}_last.log")
    with open(log_path, 'w', encoding='utf-8') as f:
        for k in log_dict:
            f.write(f"{k}: {log_dict[k]}\n")
    print(f"[INFO] Dernière valeur enregistrée dans {log_path}")

if __name__ == "__main__":
    if len(sys.argv) not in (3, 4):
        print("Usage: python test_ta_yfinance.py <SYMBOL> <INTERVAL> [crypto]")
        print("Exemple: python test_ta_yfinance.py BTC-USD 1h crypto")
        sys.exit(1)
    symbol = sys.argv[1]
    interval = sys.argv[2]
    crypto = False
    if len(sys.argv) == 4 and sys.argv[3].lower() == "crypto":
        crypto = True
    main(symbol, interval, crypto)
