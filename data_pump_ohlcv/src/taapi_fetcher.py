import os
import requests
import pandas as pd
from datetime import datetime, timedelta
import time

TAAPI_BASE_URL = "https://api.taapi.io/candles"


def fetch_ohlcv(symbol, interval, exchange, start_date, end_date, dry_run=False, log_func=print):
    """
    Récupère les OHLCV pour un symbole, un intervalle, un exchange, entre deux dates (UTC).
    Retourne un DataFrame pandas avec les colonnes : timestamp, open, high, low, close, volume
    """
    api_key = os.getenv("KEY_TAAPI_IO")
    if not api_key and not dry_run:
        raise RuntimeError("Clé API TAAPI.IO manquante dans l'environnement (KEY_TAAPI_IO)")
    # Utilise les dates passées en argument (et non utcnow)
    dt_start = datetime.strptime(start_date, "%Y-%m-%d")
    dt_end = datetime.strptime(end_date, "%Y-%m-%d") + timedelta(days=1)  # inclure toute la journée de fin
    all_rows = []
    cur_start = dt_start
    while cur_start < dt_end:
        cur_end = min(cur_start + timedelta(days=5), dt_end)
        params = {
            "secret": api_key or "DUMMY_KEY",
            "exchange": exchange,
            "symbol": symbol,
            "interval": interval,
            "start": int(cur_start.timestamp()),
            "end": int(cur_end.timestamp()),
            "backtrack": 0,
            "limit": 500
        }
        if dry_run:
            log_func(f"[DRY_RUN] Appel TAAPI.IO: {params}")
            return pd.DataFrame({
                "timestamp": [dt_start],
                "open": [1], "high": [2], "low": [0], "close": [1.5], "volume": [100]
            })
        log_func(f"[TAAPI] Appel: {params}")
        resp = requests.get(TAAPI_BASE_URL, params=params)
        if resp.status_code == 429:
            log_func("[TAAPI] Rate limit 429, sleep 5s...")
            time.sleep(5)
            continue
        resp.raise_for_status()
        data = resp.json()
        if isinstance(data, dict) and all(isinstance(v, list) for v in data.values()):
            n = len(next(iter(data.values())))
            for i in range(n):
                row = {k: v[i] for k, v in data.items()}
                all_rows.append(row)
        elif isinstance(data, list):
            all_rows.extend(data)
        elif isinstance(data, dict) and "candles" in data:
            all_rows.extend(data["candles"])
        else:
            log_func(f"[TAAPI][WARN] Format inattendu: {data}")
        cur_start = cur_end
        time.sleep(1)
    if not all_rows:
        raise RuntimeError("Aucune donnée OHLCV récupérée.")
    df = pd.DataFrame(all_rows)
    # Correction : filtrer les doublons sur le timestamp pour ne garder qu'une bougie par 15m
    if 'timestamp' in df.columns:
        df = df.drop_duplicates(subset=["timestamp"])
        df["timestamp"] = pd.to_datetime(df["timestamp"], unit="s", utc=True)
    df = df.sort_values("timestamp").reset_index(drop=True)
    return df
