import psycopg2
import json
import decimal

def to_float(val, default=None):
    if val is None:
        return default
    if isinstance(val, decimal.Decimal):
        return float(val)
    try:
        return float(val)
    except Exception:
        return default

def analyser_echecs_strategies(signal):
    resultats = {}

    # STRATEGIE 1
    try:
        rsi14 = signal.get("rsi14")
        close = signal.get("close")
        ema50 = signal.get("ema50")
        ema20 = signal.get("ema20")
        volume_relatif = signal.get("volume_relatif")
        context_spy = signal.get("context_spy")
        if None in (rsi14, close, ema50, ema20, volume_relatif, context_spy):
            resultats["strategie_1"] = "Données manquantes"
        elif not (30 <= rsi14 <= 45):
            resultats["strategie_1"] = "RSI14 hors intervalle"
        elif close < ema50:
            resultats["strategie_1"] = "Pas de cassure de EMA50"
        elif ema20 <= ema50:
            resultats["strategie_1"] = "EMA20 pas > EMA50"
        elif volume_relatif < 1.3:
            resultats["strategie_1"] = "Volume trop faible"
        elif context_spy not in ("bullish", "neutral"):
            resultats["strategie_1"] = "Contexte marché défavorable"
    except Exception as e:
        resultats["strategie_1"] = f"Erreur: {e}"

    # STRATEGIE 2
    try:
        close = to_float(signal.get("close"))
        bb_upper = to_float(signal.get("bb_upper"))
        rsi5 = to_float(signal.get("rsi5"))
        volume_relatif = to_float(signal.get("volume_relatif"))
        macd_histogram = to_float(signal.get("macd_histogram"))
        if None in (close, bb_upper, rsi5, volume_relatif, macd_histogram):
            resultats["strategie_2"] = "Données manquantes"
        elif close <= bb_upper * 1.003:
            resultats["strategie_2"] = "Pas de breakout confirmé"
        elif not (65 < rsi5 < 80):
            resultats["strategie_2"] = "RSI5 hors plage"
        elif volume_relatif <= 2:
            resultats["strategie_2"] = "Volume pas suffisant"
        elif macd_histogram <= 0:
            resultats["strategie_2"] = "MACD non haussier"
    except Exception as e:
        resultats["strategie_2"] = f"Erreur: {e}"

    # STRATEGIE 3
    try:
        if not signal.get("divergence_rsi"):
            resultats["strategie_3"] = "Pas de divergence RSI"
        elif not (signal.get("pattern_inverted_hammer") or signal.get("pattern_bullish_engulfing")):
            resultats["strategie_3"] = "Pattern non détecté"
        elif signal.get("rsi14", 100) >= 40:
            resultats["strategie_3"] = "RSI14 trop élevé"
        elif signal.get("close") >= signal.get("bb_mid", float("inf")):
            resultats["strategie_3"] = "Close trop haut"
        elif signal.get("volume_relatif", 0) < 1.2:
            resultats["strategie_3"] = "Volume trop faible"
        elif signal.get("context_spy") not in ("neutral", "rebound"):
            resultats["strategie_3"] = "Contexte défavorable"
    except Exception as e:
        resultats["strategie_3"] = f"Erreur: {e}"

    # STRATEGIE 4
    try:
        if signal.get("rsi14", 0) <= 75:
            resultats["strategie_4"] = "RSI14 pas en surachat"
        elif signal.get("close") >= signal.get("ema20", float("inf")):
            resultats["strategie_4"] = "Pas de cassure baissière"
        elif not (signal.get("pattern_bearish_engulfing") or signal.get("pattern_evening_star")):
            resultats["strategie_4"] = "Pas de pattern baissier détecté"
        elif signal.get("volume_relatif", 0) <= 1.8:
            resultats["strategie_4"] = "Volume insuffisant"
        elif signal.get("context_spy") != "bearish":
            resultats["strategie_4"] = "Contexte non baissier"
    except Exception as e:
        resultats["strategie_4"] = f"Erreur: {e}"

    # STRATEGIE 5
    try:
        bb_width = to_float(signal.get("bb_width"), 1)
        volume_relatif_moy6 = to_float(signal.get("volume_relatif_moy6"), 1)
        ema20 = to_float(signal.get("ema20"), 0)
        ema50 = to_float(signal.get("ema50"), 1)
        if bb_width is None or volume_relatif_moy6 is None or ema20 is None or ema50 is None:
            resultats["strategie_5"] = "Données manquantes"
        elif bb_width >= 0.02:
            resultats["strategie_5"] = "Pas de compression BB"
        elif volume_relatif_moy6 >= 0.8:
            resultats["strategie_5"] = "Volume relatif moyen trop élevé"
        elif abs(ema20 - ema50) / ema50 >= 0.01:
            resultats["strategie_5"] = "EMA20 trop éloignée d’EMA50"
        elif not (signal.get("pattern_doji") or signal.get("pattern_spinning_top")):
            resultats["strategie_5"] = "Pas de pattern d’indécision"
    except Exception as e:
        resultats["strategie_5"] = f"Erreur: {e}"

    return resultats


def log_echecs_signaux(echecs_signaux, log_path=None):
    import os
    from datetime import datetime
    # Correction du chemin pour compatibilité Windows et structure projet
    logs_dir = os.path.join("logs")
    os.makedirs(logs_dir, exist_ok=True)
    if log_path is None:
        now = datetime.now().strftime("%Y%m%d_%H%M%S")
        log_path = os.path.join(logs_dir, f"analyse_echecs_{now}.log")
    with open(log_path, "a", encoding="utf-8") as f:
        for signal_id, symbol, echecs in echecs_signaux:
            f.write(f"{datetime.now().isoformat()} | Signal {signal_id} | {symbol}\n")
            for strat, raison in echecs.items():
                f.write(f"   - {strat} : {raison}\n")
            f.write("\n")
    print(f"[LOG] Fichier de log généré : {log_path}")


def charger_et_analyser_signaux(conn):
    with conn.cursor() as cur:
        cur.execute("""
            SELECT * FROM signaux_bruts
        """)
        rows = cur.fetchall()
        colnames = [desc[0] for desc in cur.description]

    echecs_signaux = []
    for row in rows:
        signal = dict(zip(colnames, row))
        echecs = analyser_echecs_strategies(signal)
        if echecs:
            print(f"⛔ Signal {signal['id']} | {signal['symbol']} :")
            for strat, raison in echecs.items():
                print(f"   - {strat} : {raison}")
            echecs_signaux.append((signal['id'], signal['symbol'], echecs))
        else:
            print(f"✅ Signal {signal['id']} | {signal['symbol']} : stratégie(s) OK")
    # Toujours générer un log, même si aucun échec
    if not echecs_signaux:
        # Générer un log vide avec message explicite
        log_echecs_signaux([(-1, "AUCUN", {"info": "Aucun échec détecté sur les signaux analysés."})])
    else:
        log_echecs_signaux(echecs_signaux)


if __name__ == "__main__":
    import os
    import sys
    pc_id = "PC1000"
    try:
        conn = psycopg2.connect(
            host=os.getenv(f"BDD_VT_PG_HOST_{pc_id}"),
            port=os.getenv(f"BDD_VT_PG_PORT_{pc_id}"),
            dbname=os.getenv(f"BDD_VT_PG_DB_{pc_id}"),
            user=os.getenv(f"BDD_VT_PG_USER_VT_{pc_id}"),
            password=os.getenv(f"BDD_VT_PG_USER_VT_PASSWORD_{pc_id}")
        )
    except Exception as e:
        print(f"Erreur de connexion à la base : {e}")
        sys.exit(1)

    print(f"Connexion OK à {os.getenv(f'BDD_VT_PG_DB_{pc_id}')} sur {os.getenv(f'BDD_VT_PG_HOST_{pc_id}')} (user={os.getenv(f'BDD_VT_PG_USER_VT_{pc_id}')})")
    charger_et_analyser_signaux(conn)
    conn.close()
    print("Analyse terminée.")
