import pandas as pd
import os
import glob
from recup_signals import fetch_patterns

def detect_patterns_on_signals(input_csv, output_csv=None):
    """
    Pour chaque ligne où preliminary_valid est True, appelle TAAPI.IO pour détecter les patterns
    (cupAndHandle, ascendingTriangle, highTightFlag) et écrit le résultat dans une colonne par pattern (True/False).
    Seules les lignes preliminary_valid=True sont conservées dans le fichier de sortie.
    """
    df = pd.read_csv(input_csv)
    # Forcer preliminary_valid en booléen (important après lecture CSV)
    if df['preliminary_valid'].dtype != bool:
        df['preliminary_valid'] = df['preliminary_valid'].astype(bool)
    patterns = ['cupAndHandle', 'ascendingTriangle', 'highTightFlag']
    # Filtrer uniquement les lignes validées
    df_valid = df[df['preliminary_valid']].copy()
    # Initialiser les colonnes patterns à False
    for pat in patterns:
        df_valid[pat] = False
    # Pour chaque ligne, détecter chaque pattern séparément
    for idx, row in df_valid.iterrows():
        try:
            detected = fetch_patterns(row['actif'], interval='1d', patterns=patterns)
            print(f"[DEBUG] {row['actif']} patterns detected: {detected}")  # Ajout log debug
            for pat in patterns:
                df_valid.at[idx, pat] = (detected == pat)
        except Exception as e:
            print(f"Erreur détection pattern pour {row['actif']}: {e}")
            # On laisse False par défaut
    # Définir le nom du fichier de sortie
    if not output_csv:
        output_csv = input_csv.replace('_signals.csv', '_signals_pattern.csv')
    df_valid.to_csv(output_csv, index=False)
    print(f"Fichier patterns écrit : {output_csv}")

def find_latest_signals_csv():
    data_dir = os.path.join(os.path.dirname(__file__), '..', 'DATA')
    files = glob.glob(os.path.join(data_dir, '*_signals.csv'))
    if not files:
        print('[ERREUR] Aucun fichier _signals.csv trouvé dans DATA/')
        return None
    latest = max(files, key=os.path.getmtime)
    print(f"[INFO] Fichier _signals le plus récent détecté : {os.path.basename(latest)}")
    return latest

if __name__ == "__main__":
    import sys
    input_csv = sys.argv[1] if len(sys.argv) > 1 else None
    if not input_csv:
        input_csv = find_latest_signals_csv()
        if not input_csv:
            sys.exit(1)
    detect_patterns_on_signals(input_csv)
