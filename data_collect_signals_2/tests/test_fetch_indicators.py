import sys
import os
sys.path.insert(0, os.path.abspath(os.path.join(os.path.dirname(__file__), '..', 'src')))
from fetch_indicators import fetch_indicators
import pytest
import logging
import requests

def test_env_key_taapi_io_present():
    """Test que la clé API TAAPI.IO est bien présente dans l'environnement."""
    key = os.getenv("KEY_TAAPI_IO")
    assert key is not None and len(key) > 10, "La variable d'environnement KEY_TAAPI_IO doit être définie et non vide."

@pytest.mark.skipif(os.getenv("KEY_TAAPI_IO") is None, reason="Clé API manquante")
def test_connection_taapi_ping():
    """Test la connexion à l'API TAAPI.IO (ping simple)."""
    api_key = os.getenv("KEY_TAAPI_IO")
    print(f"[DEBUG] KEY_TAAPI_IO récupérée: {api_key}")
    url = "https://api.taapi.io/ping"
    resp = requests.get(url, params={"secret": api_key})
    assert resp.status_code == 200
    data = resp.json()
    assert data.get("status", "").lower() == "ok"

@pytest.mark.skipif(os.getenv("KEY_TAAPI_IO") is None, reason="Clé API manquante")
def test_fetch_indicators_structure():
    """Test la structure du résultat de fetch_indicators sur une action NASDAQ."""
    result = fetch_indicators("AAPL", "15m")
    assert isinstance(result, dict)
    for k in ["ema_10", "ema_21", "adx_14", "atr_14", "bollinger_upper", "bollinger_middle", "bollinger_lower"]:
        assert k in result

@pytest.mark.skipif(os.getenv("KEY_TAAPI_IO") is None, reason="Clé API manquante")
def test_fetch_indicators_btcusdt():
    result = fetch_indicators("BTC/USDT", "15m")
    assert isinstance(result, dict)
    for k in ["ema_10", "ema_21", "adx_14", "atr_14", "bollinger_upper", "bollinger_middle", "bollinger_lower"]:
        assert k in result
