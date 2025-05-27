import os
import psycopg2
import psycopg2.extras
from datetime import datetime

def make_json_safe(obj):
    if isinstance(obj, dict):
        return {k: make_json_safe(v) for k, v in obj.items()}
    elif isinstance(obj, list):
        return [make_json_safe(v) for v in obj]
    elif hasattr(obj, 'isoformat'):
        return obj.isoformat()
    else:
        return obj

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
        # Mapping patterns API -> colonnes SQL
        def get_pattern_val(signal, key):
            return signal.get(key) if key in signal else None
        # 1. Insertion de base (sans les patterns)
        insert_query = (
            "INSERT INTO signaux_bruts ("
            "symbol, dateheure, rsi14, rsi5, bb_upper, bb_lower, bb_mid, ema, close, open, high, low, pattern, "
            "eventlog, raw_json, valeur, intervalle, ema20, ema50, bb_width, volume, volume_moy20, volume_relatif, volume_relatif_moy6, macd_histogram, divergence_rsi, context_spy"
            ") VALUES ("
            + ", ".join(["%s"] * 27)
            + ") RETURNING id;"
        )
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
            psycopg2.extras.Json(make_json_safe(signal.get("raw_json"))),
            signal.get("valeur"),
            signal.get("intervalle"),
            signal.get("ema20"),
            signal.get("ema50"),
            signal.get("bb_width"),
            signal.get("volume"),
            signal.get("volume_moy20"),
            signal.get("volume_relatif"),
            signal.get("volume_relatif_moy6"),
            signal.get("macd_histogram"),
            signal.get("divergence_rsi"),
            signal.get("context_spy")
        )
        print(f"[DEBUG] Nb colonnes requête: {insert_query.count('%s')}, nb valeurs: {len(valeurs)}")
        print(f"[DEBUG] Types des valeurs: {[type(v) for v in valeurs]}")
        self.cur.execute(insert_query, valeurs)
        inserted_id = self.cur.fetchone()[0]
        # 2. UPDATE pour les patterns
        update_query = (
            "UPDATE signaux_bruts SET "
            "pattern_inverted_hammer = %s, pattern_bullish_engulfing = %s, pattern_bearish_engulfing = %s, "
            "pattern_evening_star = %s, pattern_doji = %s, pattern_spinning_top = %s "
            "WHERE id = %s"
        )
        update_vals = (
            get_pattern_val(signal, "pattern_inverted_hammer"),
            get_pattern_val(signal, "pattern_bullish_engulfing"),
            get_pattern_val(signal, "pattern_bearish_engulfing"),
            get_pattern_val(signal, "pattern_evening_star"),
            get_pattern_val(signal, "pattern_doji"),
            get_pattern_val(signal, "pattern_spinning_top"),
            inserted_id
        )
        self.cur.execute(update_query, update_vals)
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
