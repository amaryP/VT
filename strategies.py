# strategies.py
# Implémentation des conditions pour 5 stratégies graphiques

def strategie_1_pullback_rsi_ema(signal):
    return (
        30 <= signal["rsi14"] <= 45
        and signal["close"] >= signal["ema50"]
        and signal["ema20"] > signal["ema50"]
        and signal["volume_relatif"] >= 1.3
        and signal["context_spy"] in ("bullish", "neutral")
    )

def strategie_2_breakout_volume(signal):
    return (
        signal["close"] > signal["bb_upper"] * 1.003
        and 65 < signal["rsi5"] < 80
        and signal["volume_relatif"] > 2
        and signal["macd_histogram"] > 0
    )

def strategie_3_divergence_rsi_pattern(signal):
    return (
        signal["divergence_rsi"]
        and signal["pattern"] in ("inverted_hammer", "bullish_engulfing")
        and signal["rsi14"] < 40
        and signal["close"] < signal["bb_mid"]
        and signal["volume_relatif"] >= 1.2
        and signal["context_spy"] in ("neutral", "rebound")
    )

def strategie_4_short_surchat(signal):
    return (
        signal["rsi14"] > 75
        and signal["close"] < signal["ema20"]
        and signal["pattern"] in ("bearish_engulfing", "evening_star")
        and signal["volume_relatif"] > 1.8
        and signal["context_spy"] == "bearish"
    )

def strategie_5_compression_breakout(signal):
    return (
        signal["bb_width"] < 0.02
        and signal["volume_relatif_moy6"] < 0.8
        and abs(signal["ema20"] - signal["ema50"]) / signal["ema50"] < 0.01
        and signal["pattern"] in ("doji", "spinning_top")
    )

# Exemple d'utilisation
def detecter_strategies(signal):
    resultats = []
    if strategie_1_pullback_rsi_ema(signal):
        resultats.append("strategie_1")
    if strategie_2_breakout_volume(signal):
        resultats.append("strategie_2")
    if strategie_3_divergence_rsi_pattern(signal):
        resultats.append("strategie_3")
    if strategie_4_short_surchat(signal):
        resultats.append("strategie_4")
    if strategie_5_compression_breakout(signal):
        resultats.append("strategie_5")
    return resultats

# Exemple de colonnes nécessaires pour la base (PostgreSQL ou autre)
# id, symbol, dateheure, close, open, high, low
# rsi14, rsi5, ema20, ema50
# bb_upper, bb_lower, bb_mid, bb_width
# volume, volume_moy20, volume_relatif, volume_relatif_moy6
# macd_histogram, divergence_rsi (bool), pattern, context_spy
# intervalle, strategy_match[] (table ou champ jsonb)
