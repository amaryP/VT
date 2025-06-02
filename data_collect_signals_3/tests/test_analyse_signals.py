import sys
import os
import pandas as pd

sys.path.insert(0, os.path.abspath(os.path.join(os.path.dirname(__file__), '../src')))
from src.analyse_signals import compute_filters

def test_compute_filters_logic():
    df = pd.DataFrame([
        {
            "ema_50": 100, "ema_100": 90, "ema_200": 80,
            "close_price": 110, "volume_today": 2000, "volume_avg_4d": 1000, "highest_high_252d": 115
        },
        {
            "ema_50": 100, "ema_100": 110, "ema_200": 120,
            "close_price": 90, "volume_today": 500, "volume_avg_4d": 1000, "highest_high_252d": 120
        },
        {
            "ema_50": 100, "ema_100": 90, "ema_200": 80,
            "close_price": 80, "volume_today": 2000, "volume_avg_4d": 1000, "highest_high_252d": 100
        },
    ])
    df2 = compute_filters(df.copy())
    # Première ligne : tout est ok
    assert df2.loc[0, "preliminary_valid"] == True
    # Deuxième ligne : ema_50 < ema_100 < ema_200, volume_today < volume_avg_4d, close_price < 0.95*highest_high_252d
    assert df2.loc[1, "preliminary_valid"] == False
    # Troisième ligne : close_price < ema_50
    assert df2.loc[2, "preliminary_valid"] == False
    # Colonne pattern existe
    assert "pattern" in df2.columns
