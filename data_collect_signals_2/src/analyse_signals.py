import pandas as pd
import numpy as np
import os
import sys

# Par défaut, cherche le dernier fichier brut daily si aucun n'est donné
DATA_DIR = os.path.join(os.path.dirname(__file__), '../DATA')
def find_latest_csv():
    files = [f for f in os.listdir(DATA_DIR) if f.startswith('nasdaq_1d_') and f.endswith('.csv') and not f.endswith('_signals.csv')]
    if not files:
        print("[ERREUR] Aucun fichier de collecte daily trouvé dans DATA/")
        return None
    latest_file = max(files, key=lambda f: os.path.getmtime(os.path.join(DATA_DIR, f)))
    return os.path.join(DATA_DIR, latest_file)

def compute_filters(df):
    # Filtres individuels
    df['filter_croisement_ema'] = df['ema_10'] > df['ema_21']
    df['filter_force_tendance_adx'] = df['adx_14'] > 25
    df['filter_breakout_bollinger'] = df['close'] > df['bollinger_upper']
    df['filter_volume_significatif'] = df['volume'] > df['sma_volume_20']
    df['filter_rsi_stable'] = df['rsi_14'] < 70
    df['filter_volatilite_atr'] = (df['close'] - df['open']).abs() > (1.5 * df['atr_14'])
    # --- Stratégie TrendSwing US ---
    df['filter_close_above_ma50'] = df['close'] > df['ma50']
    df['filter_ma20_above_ma50'] = df['ma20'] > df['ma50']
    df['filter_rsi_trendswing'] = df['rsi_14'] > 55
    df['filter_adx_trendswing'] = df['adx_14'] > 20
    df['filter_volume_trendswing'] = df['volume'] > df['ma_volume_20']
    df['signal_trendswing_us'] = (
        df['filter_close_above_ma50'] &
        df['filter_ma20_above_ma50'] &
        df['filter_rsi_trendswing'] &
        df['filter_adx_trendswing'] &
        df['filter_volume_trendswing']
    )
    # Filtre combiné complet
    df['signal'] = (
        df['filter_croisement_ema'] &
        df['filter_force_tendance_adx'] &
        df['filter_breakout_bollinger'] &
        df['filter_volume_significatif'] &
        df['filter_rsi_stable'] &
        df['filter_volatilite_atr']
    )
    # Stratégie partielle core trend only
    df['signal_core'] = (
        df['filter_croisement_ema'] &
        df['filter_force_tendance_adx'] &
        df['filter_volatilite_atr']
    )
    return df

def main(input_csv=None):
    if input_csv is None:
        input_csv = find_latest_csv()
        if input_csv is None:
            return
    output_csv = input_csv.replace('.csv', '_signals.csv')
    print(f"[INFO] Fichier à analyser : {input_csv}")
    df = pd.read_csv(input_csv)
    print(f"Nombre de lignes chargées : {len(df)}")
    num_cols = [
        'ema_10', 'ema_21', 'adx_14', 'atr_14',
        'bollinger_upper', 'open', 'close', 'volume', 'sma_volume_20', 'rsi_14',
        'ma20', 'ma50', 'ma_volume_20'
    ]
    for col in num_cols:
        if col in df.columns:
            print(f"Conversion de la colonne : {col}")
            df[col] = pd.to_numeric(df[col], errors='coerce')
    print("Application des filtres...")
    df = compute_filters(df)
    print("Écriture du fichier enrichi...")
    df.to_csv(output_csv, index=False)
    print(f"Fichier enrichi écrit : {output_csv}")

if __name__ == "__main__":
    input_csv = sys.argv[1] if len(sys.argv) > 1 else None
    main(input_csv)
