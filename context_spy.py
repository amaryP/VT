import os
import requests

def get_context_spy(interval: str, crypto: bool = False) -> str:
    """
    Récupère les deux derniers close du SPY sur l’intervalle donné,
    calcule la variation %, et retourne le contexte : 'bullish', 'neutral', 'bearish'.
    Si crypto=True, retourne toujours 'neutral' (aucun contexte macro pour crypto).
    """
    if crypto:
        return "neutral"

    TAAPI_SECRET = os.getenv("TAAPI_SECRET") or os.getenv("KEY_TAAPI_IO")
    if not TAAPI_SECRET:
        raise RuntimeError("TAAPI_SECRET ou KEY_TAAPI_IO non défini dans les variables d'environnement.")

    # Pour SPY (stock/index), il faut utiliser type=stocks et ne PAS mettre exchange
    try:
        url_now = (
            f"https://api.taapi.io/price"
            f"?secret={TAAPI_SECRET}"
            f"&symbol=SPY"
            f"&interval={interval}"
            f"&type=stocks"
            f"&backtrack=0"
        )
        resp_now = requests.get(url_now)
        resp_now.raise_for_status()
        data_now = resp_now.json()
        close_now = data_now["value"]

        url_prev = (
            f"https://api.taapi.io/price"
            f"?secret={TAAPI_SECRET}"
            f"&symbol=SPY"
            f"&interval={interval}"
            f"&type=stocks"
            f"&backtrack=1"
        )
        resp_prev = requests.get(url_prev)
        resp_prev.raise_for_status()
        data_prev = resp_prev.json()
        close_prev = data_prev["value"]

        variation_pct = ((close_now - close_prev) / close_prev) * 100

        if variation_pct > 1.5:
            return "bullish"
        elif variation_pct < -1.5:
            return "bearish"
        else:
            return "neutral"
    except Exception as e:
        raise RuntimeError(f"Impossible de récupérer le contexte SPY sur taapi.io (type=stocks): {e}")

if __name__ == "__main__":
    interval = "15m"
    context = get_context_spy(interval)
    print(f"Contexte SPY ({interval}) : {context}")
