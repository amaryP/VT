import pandas as pd
import numpy as np
import os

# Fichier source (à adapter si besoin)
INPUT_CSV = os.path.join(os.path.dirname(__file__), '../DATA/nasdaq_15m_20250528T1657.csv')
OUTPUT_CSV = os.path.join(os.path.dirname(__file__), '../DATA/nasdaq_15m_20250528T1657_signals.csv')

def compute_filters(df):
    # Filtres individuels
    df['filter_croisement_ema'] = df['ema_10'] > df['ema_21']
    df['filter_force_tendance_adx'] = df['adx_14'] > 25
    df['filter_breakout_bollinger'] = df['close'] > df['bollinger_upper']
    df['filter_volume_significatif'] = df['volume'] > df['sma_volume_20']
    df['filter_rsi_stable'] = df['rsi_14'] < 70
    df['filter_volatilite_atr'] = (df['close'] - df['open']).abs() > (1.5 * df['atr_14'])
    # Filtre combiné
    df['signal'] = (
        df['filter_croisement_ema'] &
        df['filter_force_tendance_adx'] &
        df['filter_breakout_bollinger'] &
        df['filter_volume_significatif'] &
        df['filter_rsi_stable'] &
        df['filter_volatilite_atr']
    )
    return df

def main():
    # 1. Collecte des données (génère le fichier brut)
    import importlib.util
    main_collect_path = os.path.abspath(os.path.join(os.path.dirname(__file__), 'main_collect.py'))
    spec = importlib.util.spec_from_file_location("main_collect", main_collect_path)
    main_collect = importlib.util.module_from_spec(spec)
    spec.loader.exec_module(main_collect)
    collecte_main = main_collect.main
    print("[INFO] Lancement de la collecte des indicateurs...")
    collecte_main()
    # Recherche du dernier fichier généré (par convention de nommage)
    data_dir = os.path.join(os.path.dirname(__file__), '../DATA')
    files = [f for f in os.listdir(data_dir) if f.startswith('nasdaq_15m_') and f.endswith('.csv') and not f.endswith('_signals.csv')]
    if not files:
        print("[ERREUR] Aucun fichier de collecte trouvé dans DATA/")
        return
    latest_file = max(files, key=lambda f: os.path.getmtime(os.path.join(data_dir, f)))
    input_csv = os.path.join(data_dir, latest_file)
    output_csv = input_csv.replace('.csv', '_signals.csv')
    print(f"[INFO] Fichier à analyser : {input_csv}")
    # 2. Analyse des signaux
    df = pd.read_csv(input_csv)
    print(f"Nombre de lignes chargées : {len(df)}")
    num_cols = [
        'ema_10', 'ema_21', 'adx_14', 'atr_14',
        'bollinger_upper', 'open', 'close', 'volume', 'sma_volume_20', 'rsi_14'
    ]
    for col in num_cols:
        print(f"Conversion de la colonne : {col}")
        df[col] = pd.to_numeric(df[col], errors='coerce')
    print("Application des filtres...")
    df = compute_filters(df)
    print("Écriture du fichier enrichi...")
    df.to_csv(output_csv, index=False)
    print(f"Fichier enrichi écrit : {output_csv}")
    # Fin du script, pas de relance ou boucle

if __name__ == "__main__":
    main()
