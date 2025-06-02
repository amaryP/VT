import sys
import os
import csv
import tempfile
sys.path.insert(0, os.path.abspath(os.path.join(os.path.dirname(__file__), '../src')))
from src.main_collect import main

def test_main_collect(monkeypatch):
    # Prépare une wishlist temporaire
    wishlist = tempfile.NamedTemporaryFile(mode="w", delete=False, encoding="utf-8")
    wishlist.write("AAPL\nMSFT\n")
    wishlist.close()
    # Mock fetch_indicators pour renvoyer des valeurs fixes
    from src import recup_signals
    def fake_fetch_indicators(symbol, interval, **kwargs):
        return {
            "ema_50": 100, "ema_100": 90, "ema_200": 80,
            "close_price": 110, "volume_today": 2000, "volume_avg_4d": 1000, "highest_high_252d": 115
        }
    monkeypatch.setattr(recup_signals, "fetch_indicators", fake_fetch_indicators)
    # Patch le chemin de la wishlist
    import src.main_collect as main_collect_mod
    main_collect_mod.DATA_DIR = os.path.dirname(wishlist.name)
    # Patch le nom du fichier de sortie pour éviter d'écrire dans DATA/
    orig_os_path_join = os.path.join
    def fake_path_join(*args):
        if args[-1].startswith("nasdaq_"):
            return tempfile.mktemp(suffix=".csv")
        return orig_os_path_join(*args)
    monkeypatch.setattr(os.path, "join", fake_path_join)
    # Exécute
    main()
    # Vérifie qu'un fichier a été créé avec les bonnes colonnes
    outfiles = [f for f in os.listdir(tempfile.gettempdir()) if f.startswith("nasdaq_") and f.endswith(".csv")]
    assert outfiles, "Aucun fichier de sortie généré"
    with open(os.path.join(tempfile.gettempdir(), outfiles[-1]), encoding="utf-8") as f:
        reader = csv.reader(f)
        header = next(reader)
        assert header == [
            'actif', 'datetime',
            'ema_50', 'ema_100', 'ema_200',
            'close_price', 'volume_today', 'volume_avg_4d', 'highest_high_252d'
        ]
        # Vérifie qu'il y a au moins une ligne de données non vide
        data_rows = list(reader)
        assert any(any(cell not in (None, '', 'NaN') for cell in row[2:]) for row in data_rows), "Aucune donnée collectée dans le CSV !"
    os.remove(wishlist.name)
    os.remove(os.path.join(tempfile.gettempdir(), outfiles[-1]))
