import os
import psycopg2
import psycopg2.extras
from datetime import datetime

class PgLogger:
    def __init__(self):
        pc_id = "PC1000"
        self.conn = psycopg2.connect(
            host=os.getenv(f"BDD_VT_PG_HOST_{pc_id}"),
            port=os.getenv(f"BDD_VT_PG_PORT_{pc_id}"),
            dbname=os.getenv(f"BDD_VT_PG_DB_{pc_id}"),
            user=os.getenv(f"BDD_VT_PG_USER_VT_{pc_id}"),
            password=os.getenv(f"BDD_VT_PG_USER_VT_PASSWORD_{pc_id}")
        )
        self.cur = self.conn.cursor()

    def log_signal_brut(self, signal: dict):
        insert_query = """
        INSERT INTO signaux_bruts (symbol, dateheure, rsi14, rsi5, bb_upper, bb_lower, bb_mid, ema, close, open, high, low, pattern, eventlog, raw_json, valeur, intervalle)
        VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s)
        RETURNING id;
        """
        valeurs = (
            signal.get("symbol"),
            signal.get("dateheure", datetime.now()),
            signal.get("rsi14"),
            signal.get("rsi5"),
            signal.get("bb_upper"),
            signal.get("bb_lower"),
            signal.get("bb_mid"),
            signal.get("ema"),
            signal.get("close"),
            signal.get("open"),
            signal.get("high"),
            signal.get("low"),
            signal.get("pattern"),
            signal.get("eventlog"),
            psycopg2.extras.Json(signal),
            signal.get("valeur"),
            signal.get("intervalle")
        )
        self.cur.execute(insert_query, valeurs)
        inserted_id = self.cur.fetchone()[0]
        self.conn.commit()
        return inserted_id

    def log_evenement(self, evenement: dict):
        insert_query = """
        INSERT INTO evenementset (symbol, dateheure, valeur, rsi14, rsi5, figure, eventlog)
        VALUES (%s, %s, %s, %s, %s, %s, %s)
        RETURNING id;
        """
        valeurs = (
            evenement.get("symbol"),
            evenement.get("dateheure", datetime.now()),
            evenement.get("valeur"),
            evenement.get("rsi14"),
            evenement.get("rsi5"),
            evenement.get("figure"),
            evenement.get("eventlog")
        )
        self.cur.execute(insert_query, valeurs)
        inserted_id = self.cur.fetchone()[0]
        self.conn.commit()
        return inserted_id

    def close(self):
        self.cur.close()
        self.conn.close()
