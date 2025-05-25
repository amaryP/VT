import os
import psycopg2

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
    # Exécuter une requête pour lire les 10 premières lignes
    cur.execute("SELECT * FROM evenementset LIMIT 10;")
    rows = cur.fetchall()

    print("Données de la table evenementset (max 10 lignes) :")
    for row in rows:
        print(row)

    cur.close()
    conn.close()

except Exception as e:
    print(f"Erreur lors de la connexion ou de la lecture : {e}")