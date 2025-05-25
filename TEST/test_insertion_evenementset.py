import os
import psycopg2
from datetime import datetime

pc_id = "PC1000"

# Récupération des variables d'environnement
host = os.getenv(f"BDD_VT_PG_HOST_{pc_id}")
port = os.getenv(f"BDD_VT_PG_PORT_{pc_id}")
dbname = os.getenv(f"BDD_VT_PG_DB_{pc_id}")
user = os.getenv(f"BDD_VT_PG_USER_VT_{pc_id}")
password = os.getenv(f"BDD_VT_PG_USER_VT_PASSWORD_{pc_id}")

try:
    conn = psycopg2.connect(
        host=host,
        port=port,
        dbname=dbname,
        user=user,
        password=password
    )
    print(f"Connexion réussie à la base {dbname} sur {host}:{port}")
    cur = conn.cursor()

    # Exemple d'insertion adapté à la structure de la table evenementset
    insert_query = """
    INSERT INTO evenementset (symbol, dateheure, valeur, rsi14, rsi5, figure, eventlog)
    VALUES (%s, %s, %s, %s, %s, %s, %s)
    RETURNING *;
    """
    valeurs = (
        "BTC/USDT",                # symbol
        datetime.now(),             # dateheure
        26425.37,                   # valeur
        28.72,                      # rsi14
        33.10,                      # rsi5
        "hammer",                  # figure
        "RSI14<30 + BBDown + pattern marteau"  # eventlog
    )
    cur.execute(insert_query, valeurs)
    inserted_row = cur.fetchone()
    conn.commit()
    print("Ligne insérée :", inserted_row)

    # Vérification de l'insertion
    cur.execute("SELECT * FROM evenementset LIMIT 10;")
    rows = cur.fetchall()
    print("Données de la table evenementset (max 10 lignes) :")
    for row in rows:
        print(row)

    cur.close()
    conn.close()

except Exception as e:
    print(f"Erreur lors de la connexion, de l'insertion ou de la lecture : {e}")
