import sys
import os
import pandas as pd
sys.path.insert(0, os.path.abspath(os.path.join(os.path.dirname(__file__), '../src')))
from file_manager import save_ohlcv_csv

def test_save_ohlcv_csv(tmp_path):
    # Création d'un DataFrame factice
    df = pd.DataFrame({
        "timestamp": ["2024-05-20T00:00:00Z"],
        "open": [1], "high": [2], "low": [0], "close": [1.5], "volume": [100]
    })
    save_ohlcv_csv(df, "BTC/USDT", "15m", "binance", "2024-05-20", "2024-05-21")
    # Vérifie que le fichier a bien été créé
    dirpath = os.path.join(os.path.dirname(__file__), "..", "data", "raw", "crypto", "BINANCE")
    files = os.listdir(dirpath)
    assert any(f.startswith("BTCUSDT_15m_BINANCE_2024-05-20_2024-05-21") for f in files)
