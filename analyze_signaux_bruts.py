import os
import psycopg2
import psycopg2.extras
import json
from strategies import (
    strategie_1_pullback_rsi_ema,
    strategie_2_breakout_volume,
    strategie_3_divergence_rsi_pattern,
    strategie_4_short_surchat,
    strategie_5_compression_breakout
)

STRATEGY_LIST = [
    ("strategie_1", strategie_1_pullback_rsi_ema),
    ("strategie_2", strategie_2_breakout_volume),
    ("strategie_3", strategie_3_divergence_rsi_pattern),
    ("strategie_4", strategie_4_short_surchat),
    ("strategie_5", strategie_5_compression_breakout)
]

def get_pg_conn():
    pc_id = "PC1000"
    return psycopg2.connect(
        host=os.getenv(f"BDD_VT_PG_HOST_{pc_id}"),
        port=os.getenv(f"BDD_VT_PG_PORT_{pc_id}"),
        dbname=os.getenv(f"BDD_VT_PG_DB_{pc_id}"),
        user=os.getenv(f"BDD_VT_PG_USER_VT_{pc_id}"),
        password=os.getenv(f"BDD_VT_PG_USER_VT_PASSWORD_{pc_id}")
    )

def fetch_signaux_bruts_a_analyser(cur):
    cur.execute("SELECT * FROM signaux_bruts WHERE strategy_match IS NULL")
    return cur.fetchall(), [desc[0] for desc in cur.description]

def update_strategy_match(cur, signal_id, match):
    cur.execute(
        "UPDATE signaux_bruts SET strategy_match = %s WHERE id = %s",
        (json.dumps(match) if isinstance(match, list) else match, signal_id)
    )

def signal_row_to_dict(row, columns):
    return dict(zip(columns, row))

def convert_signal_types(signal):
    float_fields = [
        "rsi14", "rsi5", "ema20", "ema50", "ema", "bb_upper", "bb_lower", "bb_mid", "bb_width",
        "close", "open", "high", "low", "volume", "volume_moy20", "volume_relatif", "volume_relatif_moy6",
        "macd_histogram", "valeur"
    ]
    for field in float_fields:
        if field in signal and signal[field] is not None:
            try:
                signal[field] = float(signal[field])
            except Exception:
                pass
    return signal

def try_detect_strategies(signal):
    matches = []
    for name, func in STRATEGY_LIST:
        if func(signal):
            matches.append(name)
    return matches

def main():
    conn = get_pg_conn()
    cur = conn.cursor()
    rows, columns = fetch_signaux_bruts_a_analyser(cur)
    print(f"[INFO] {len(rows)} signaux à analyser.")
    for row in rows:
        signal = signal_row_to_dict(row, columns)
        signal = convert_signal_types(signal)
        matches = try_detect_strategies(signal)
        if matches:
            update_strategy_match(cur, signal['id'], matches)
            print(f"[OK] Signal id={signal['id']} : stratégies matchées {matches}")
        else:
            update_strategy_match(cur, signal['id'], json.dumps("DONE"))
            print(f"[OK] Signal id={signal['id']} : aucune stratégie matchée (DONE)")
    conn.commit()
    cur.close()
    conn.close()
    print("[INFO] Analyse terminée.")

if __name__ == "__main__":
    main()
