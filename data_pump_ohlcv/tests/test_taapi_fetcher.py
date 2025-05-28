import sys
import os
import pytest
sys.path.insert(0, os.path.abspath(os.path.join(os.path.dirname(__file__), '../src')))
from taapi_fetcher import fetch_ohlcv

def test_fetch_ohlcv(monkeypatch):
    # Test DRY_RUN (ne fait pas d'appel réel)
    calls = []
    def fake_log(msg):
        calls.append(msg)
    df = fetch_ohlcv("BTC/USDT", "15m", "binance", "2024-05-20", "2024-05-21", dry_run=True, log_func=fake_log)
    assert calls, "Le log doit être appelé en DRY_RUN"
    # Test structure DataFrame (simulation)
    # On ne teste pas l'appel réel ici pour éviter quota/clé
