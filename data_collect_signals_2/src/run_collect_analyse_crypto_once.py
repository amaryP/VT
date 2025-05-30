import os
import sys
from datetime import datetime, timezone
import importlib.util

SRC_DIR = os.path.dirname(__file__)
sys.path.append(SRC_DIR)
DATA_DIR = os.path.abspath(os.path.join(SRC_DIR, '../DATA'))
WISHLIST = os.path.abspath(os.path.join(SRC_DIR, '../../wishlist_symbols_crypto.txt'))

def load_symbols():
    with open(WISHLIST, 'r', encoding='utf-8') as f:
        return [line.strip() for line in f if line.strip() and not line.startswith('#')]

def main():
    symbols = load_symbols()
    interval = '15m'
    now = datetime.now(timezone.utc).replace(second=0, microsecond=0)
    out_name = f"crypto_{interval}_{now.strftime('%Y%m%dT%H%M')}.csv"
    out_path = os.path.join(DATA_DIR, out_name)
    # Import dynamique de recup_signals
    spec = importlib.util.spec_from_file_location("recup_signals", os.path.join(SRC_DIR, 'recup_signals.py'))
    fetch_mod = importlib.util.module_from_spec(spec)
    spec.loader.exec_module(fetch_mod)
    fetch_indicators = fetch_mod.fetch_indicators
    # Import du wrapper retry
    spec2 = importlib.util.spec_from_file_location("main_collect", os.path.join(SRC_DIR, 'main_collect.py'))
    main_collect_mod = importlib.util.module_from_spec(spec2)
    spec2.loader.exec_module(main_collect_mod)
    call_taapi_with_retry = main_collect_mod.call_taapi_with_retry
    import csv
    with open(out_path, 'w', newline='', encoding='utf-8') as fcsv:
        writer = csv.writer(fcsv)
        writer.writerow([
            'actif', 'datetime',
            'ema_10', 'ema_21', 'adx_14', 'atr_14',
            'bollinger_upper', 'bollinger_middle', 'bollinger_lower',
            'open', 'close', 'volume', 'sma_volume_20', 'rsi_14'
        ])
        total = len(symbols)
        for idx, symbol in enumerate(symbols, 1):
            try:
                print(f"[{idx}/{total}] Collecte {symbol}...")
                indicators = call_taapi_with_retry(fetch_indicators, symbol, interval, debug=True)
                if indicators is None or not any(indicators.values()):
                    print(f"[{idx}/{total}] ATTENTION: Résultat vide pour {symbol}")
                writer.writerow([
                    symbol,
                    now.isoformat(),
                    indicators.get('ema_10') if indicators else None,
                    indicators.get('ema_21') if indicators else None,
                    indicators.get('adx_14') if indicators else None,
                    indicators.get('atr_14') if indicators else None,
                    indicators.get('bollinger_upper') if indicators else None,
                    indicators.get('bollinger_middle') if indicators else None,
                    indicators.get('bollinger_lower') if indicators else None,
                    indicators.get('open') if indicators else None,
                    indicators.get('close') if indicators else None,
                    indicators.get('volume') if indicators else None,
                    indicators.get('sma_volume_20') if indicators else None,
                    indicators.get('rsi_14') if indicators else None
                ])
            except Exception as e:
                print(f"[{idx}/{total}] Erreur pour {symbol}: {e}")
            import time; time.sleep(0.5)
    print(f"[INFO] Fichier collecte écrit: {out_path}")
    # 2. Analyse
    spec3 = importlib.util.spec_from_file_location("analyse_signals", os.path.join(SRC_DIR, 'analyse_signals.py'))
    analyse_signals = importlib.util.module_from_spec(spec3)
    spec3.loader.exec_module(analyse_signals)
    analyse_signals.main(input_csv=out_path)

if __name__ == "__main__":
    main()
