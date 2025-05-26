# strategies.py
# Implémentation des conditions pour 5 stratégies graphiques

def strategie_1_pullback_rsi_ema(signal):
    rsi14 = signal.get("rsi14")
    close = signal.get("close")
    ema50 = signal.get("ema50")
    ema20 = signal.get("ema20")
    volume_relatif = signal.get("volume_relatif")
    context_spy = signal.get("context_spy")
    if None in (rsi14, close, ema50, ema20, volume_relatif, context_spy):
        return False
    return (
        30 <= rsi14 <= 45
        and close >= ema50
        and ema20 > ema50
        and volume_relatif >= 1.3
        and context_spy in ("bullish", "neutral")
    )

def strategie_2_breakout_volume(signal):
    close = signal.get("close")
    bb_upper = signal.get("bb_upper")
    rsi5 = signal.get("rsi5")
    volume_relatif = signal.get("volume_relatif")
    macd_histogram = signal.get("macd_histogram")
    if None in (close, bb_upper, rsi5, volume_relatif, macd_histogram):
        return False
    return (
        close > bb_upper * 1.003
        and 65 < rsi5 < 80
        and volume_relatif > 2
        and macd_histogram > 0
    )

def strategie_3_divergence_rsi_pattern(signal):
    divergence_rsi = signal.get("divergence_rsi")
    rsi14 = signal.get("rsi14")
    close = signal.get("close")
    bb_mid = signal.get("bb_mid")
    volume_relatif = signal.get("volume_relatif")
    context_spy = signal.get("context_spy")
    pattern_inverted_hammer = signal.get("pattern_inverted_hammer")
    pattern_bullish_engulfing = signal.get("pattern_bullish_engulfing")
    if None in (divergence_rsi, rsi14, close, bb_mid, volume_relatif, context_spy):
        return False
    if not (pattern_inverted_hammer or pattern_bullish_engulfing):
        return False
    return (
        divergence_rsi
        and rsi14 < 40
        and close < bb_mid
        and volume_relatif >= 1.2
        and context_spy in ("neutral", "rebound")
    )

def strategie_4_short_surchat(signal):
    rsi14 = signal.get("rsi14")
    close = signal.get("close")
    ema20 = signal.get("ema20")
    volume_relatif = signal.get("volume_relatif")
    context_spy = signal.get("context_spy")
    pattern_bearish_engulfing = signal.get("pattern_bearish_engulfing")
    pattern_evening_star = signal.get("pattern_evening_star")
    if None in (rsi14, close, ema20, volume_relatif, context_spy):
        return False
    if not (pattern_bearish_engulfing or pattern_evening_star):
        return False
    return (
        rsi14 > 75
        and close < ema20
        and volume_relatif > 1.8
        and context_spy == "bearish"
    )

def strategie_5_compression_breakout(signal):
    bb_width = signal.get("bb_width")
    volume_relatif_moy6 = signal.get("volume_relatif_moy6")
    ema20 = signal.get("ema20")
    ema50 = signal.get("ema50")
    pattern_doji = signal.get("pattern_doji")
    pattern_spinning_top = signal.get("pattern_spinning_top")
    if None in (bb_width, volume_relatif_moy6, ema20, ema50):
        return False
    if not (pattern_doji or pattern_spinning_top):
        return False
    return (
        bb_width < 0.02
        and volume_relatif_moy6 < 0.8
        and abs(ema20 - ema50) / ema50 < 0.01
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
