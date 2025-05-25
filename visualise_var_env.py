import os
from dotenv import load_dotenv

# Charge le fichier .env situé à la racine du projet
load_dotenv()

pc_id = "PC1000"

pg_host = os.getenv(f"BDD_VT_PG_HOST_{pc_id}")
pg_port = os.getenv(f"BDD_VT_PG_PORT_{pc_id}")
pg_db = os.getenv(f"BDD_VT_PG_DB_{pc_id}")
pg_super_user = os.getenv(f"BDD_VT_PG_SUPER_USER_{pc_id}")
pg_super_user_password = os.getenv(f"BDD_VT_PG_SUPER_USER_PASSWORD_{pc_id}")
pg_user = os.getenv(f"BDD_VT_PG_USER_VT_{pc_id}")
pg_user_password = os.getenv(f"BDD_VT_PG_USER_VT_PASSWORD_{pc_id}")

print(f"Connexion PostgreSQL sur PC {pc_id} :")
print(f"Host: {pg_host}")
print(f"Port: {pg_port}")
print(f"Base: {pg_db}")
print(f"Super user: {pg_super_user}")
