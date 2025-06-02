import sys, os
sys.path.insert(0, os.path.abspath(os.path.join(os.path.dirname(__file__), '../src')))
from src.collect_patterns import detect_patterns_on_signals
import pandas as pd
import tempfile

def test_detect_patterns_on_signals(monkeypatch):
    # Prépare un DataFrame de test
    df = pd.DataFrame([
        {"actif": "AAPL", "preliminary_valid": True},
        {"actif": "MSFT", "preliminary_valid": False},
        {"actif": "GOOG", "preliminary_valid": True},
    ])
    with tempfile.NamedTemporaryFile(suffix=".csv", delete=False) as f:
        df.to_csv(f.name, index=False)
        test_csv = f.name
    # Mock fetch_patterns pour renvoyer un pattern ou None
    from src import recup_signals
    def fake_fetch_patterns(symbol, interval, patterns, **kwargs):
        return "cupAndHandle" if symbol == "AAPL" else None
    monkeypatch.setattr(recup_signals, "fetch_patterns", fake_fetch_patterns)
    # Appel
    detect_patterns_on_signals(test_csv)
    # Vérifie le résultat
    df2 = pd.read_csv(test_csv.replace('.csv', '_patterns.csv'))
    assert df2.loc[df2["actif"] == "AAPL", "pattern"].iloc[0] == "cupAndHandle"
    assert pd.isna(df2.loc[df2["actif"] == "MSFT", "pattern"].iloc[0])
    assert pd.isna(df2.loc[df2["actif"] == "GOOG", "pattern"].iloc[0])
    os.remove(test_csv)
    os.remove(test_csv.replace('.csv', '_patterns.csv'))
