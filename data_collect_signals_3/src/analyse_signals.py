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
    # Filtrage technique filtered_pattern_breakout
    df['preliminary_valid'] = (
        (df['close_price'] > df['ema_50']) &
        (df['ema_50'] > df['ema_100']) &
        (df['ema_100'] > df['ema_200']) &
        (df['volume_today'] > df['volume_avg_4d']) &
        (df['close_price'] > 0.95 * df['highest_high_252d'])
    )
    # Préparation colonne pattern (sera remplie plus tard)
    df['pattern'] = None
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
        'ema_50', 'ema_100', 'ema_200',
        'close_price', 'volume_today', 'volume_avg_4d', 'highest_high_252d'
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
