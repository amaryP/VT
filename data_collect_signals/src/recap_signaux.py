import os
import glob
import csv

DATA_DIR = os.path.join(os.path.dirname(__file__), '../DATA')
RECAP_FILE = os.path.join(DATA_DIR, 'recap_signaux.csv')

# Cherche tous les fichiers *_signals.csv dans DATA
signal_files = glob.glob(os.path.join(DATA_DIR, '*_signals.csv'))

recap_rows = []
header = None

for file in signal_files:
    with open(file, newline='', encoding='utf-8') as f:
        reader = csv.reader(f)
        file_header = next(reader)
        # On prend le header du premier fichier
        if header is None:
            header = file_header
        for row in reader:
            # On vérifie les deux dernières colonnes
            if len(row) >= 2:
                val1 = row[-2].strip().lower() == 'true'
                val2 = row[-1].strip().lower() == 'true'
                if val1 or val2:
                    recap_rows.append(row)

# Écrit le fichier récapitulatif
with open(RECAP_FILE, 'w', newline='', encoding='utf-8') as f:
    writer = csv.writer(f)
    if header:
        writer.writerow(header)
    writer.writerows(recap_rows)

print(f"Fichier récapitulatif créé : {RECAP_FILE} ({len(recap_rows)} lignes)")
