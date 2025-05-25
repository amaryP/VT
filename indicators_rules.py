# Définition des règles de détection pour les principaux indicateurs
# Peut être enrichi facilement

# === RÈGLES UNITAIRES ===
def evaluate_rsi(rsi, survente=30, surachat=70):
    """
    Retourne l'état du RSI : 'survente', 'surachat', 'neutre' ou None.
    """
    if rsi is None:
        return None
    if rsi < survente:
        return "survente"
    elif rsi > surachat:
        return "surachat"
    else:
        return "neutre"

def evaluate_macd(macd, macd_signal):
    """
    Retourne l'état du MACD : 'bullish_cross', 'bearish_cross', 'flat' ou None.
    """
    if macd is None or macd_signal is None:
        return None
    if macd > macd_signal:
        return "bullish_cross"
    elif macd < macd_signal:
        return "bearish_cross"
    else:
        return "flat"

def evaluate_bbands(close, bb_upper, bb_lower):
    """
    Retourne la position du cours par rapport aux bandes de Bollinger :
    'au-dessus_bollinger', 'en-dessous_bollinger', 'dans_bollinger' ou None.
    """
    if close is None or bb_upper is None or bb_lower is None:
        return None
    if close > bb_upper:
        return "au-dessus_bollinger"
    elif close < bb_lower:
        return "en-dessous_bollinger"
    else:
        return "dans_bollinger"

# === RÈGLES COMBINÉES (SIGNAL GLOBAL) ===
def evaluate_signal_combined(rsi14, rsi5, close, bb_lower, bb_upper=None, macd=None, macd_signal=None):
    """
    Retourne un signal d'action basé sur les règles combinées les plus probantes issues des notes :
    - RSI14 < 30
    - RSI5 > RSI14
    - close < BB Lower
    - (optionnel) croisement MACD
    """
    if rsi14 is None or rsi5 is None or close is None or bb_lower is None:
        return None
    # Signal d'achat fort : RSI14 < 30, RSI5 > RSI14, close < BB Lower
    if rsi14 < 30 and rsi5 > rsi14 and close < bb_lower:
        return "signal_achat_strong"
    # Signal d'achat modéré : RSI14 < 30, RSI5 > RSI14
    if rsi14 < 30 and rsi5 > rsi14:
        return "signal_achat_moderate"
    # Signal de croisement RSI5/RSI14
    if rsi5 > rsi14:
        return "signal_achat_rsi_cross"
    # Signal de survente simple
    if rsi14 < 30:
        return "signal_survente"
    # (Optionnel) MACD croisement haussier
    if macd is not None and macd_signal is not None and macd > macd_signal:
        return "signal_macd_bullish"
    return None

# === MAPPING DES SIGNIFICATIONS (pour extension future/action automatisée) ===
SIGNAL_ACTIONS = {
    "signal_achat_strong": "Achat prioritaire (RSI14<30, RSI5>RSI14, close<BBLower)",
    "signal_achat_moderate": "Achat modéré (RSI14<30, RSI5>RSI14)",
    "signal_achat_rsi_cross": "Achat sur croisement RSI5>RSI14",
    "signal_survente": "Surveillance survente RSI14<30",
    "signal_macd_bullish": "Momentum haussier MACD"
}

# === EXEMPLES D'UTILISATION ===
# rsi_signal = evaluate_rsi(rsi14)
# macd_signal = evaluate_macd(macd, macd_signal)
# bbands_signal = evaluate_bbands(close, bb_upper, bb_lower)
# signal = evaluate_signal_combined(rsi14, rsi5, close, bb_lower, bb_upper, macd, macd_signal)
# action = SIGNAL_ACTIONS.get(signal)
