# calcul_indicateurs.py
"""
Script utilitaire pour calculer les indicateurs dérivés non fournis directement par l'API taapi.io.
- volume_moy20 : moyenne des 20 derniers volumes
- volume_relatif : volume / volume_moy20
- volume_relatif_moy6 : moyenne des 6 derniers volume_relatif
- divergence_rsi : détection simple de divergence (exemple)
- context_spy : exemple de mapping (à adapter)
"""
from typing import List, Optional

def volume_moyenne(volumes: List[float], n: int = 20) -> Optional[float]:
    if len(volumes) < n:
        return None
    return sum(volumes[-n:]) / n

def volume_relatif(volume: float, volume_moy20: Optional[float]) -> Optional[float]:
    if not volume_moy20 or volume_moy20 == 0:
        return None
    return volume / volume_moy20

def volume_relatif_moyenne(volume_relatifs: List[float], n: int = 6) -> Optional[float]:
    if len(volume_relatifs) < n:
        return None
    return sum(volume_relatifs[-n:]) / n

def detect_divergence_rsi(prices: List[float], rsis: List[float]) -> bool:
    # Exemple simple : divergence si prix fait un plus bas mais RSI fait un plus haut
    if len(prices) < 3 or len(rsis) < 3:
        return False
    return (prices[-1] < prices[-2] < prices[-3]) and (rsis[-1] > rsis[-2] > rsis[-3])

def context_spy_from_value(val: float) -> str:
    # Exemple de mapping (à adapter selon ta logique)
    if val > 1.5:
        return "bullish"
    elif val < -1.5:
        return "bearish"
    else:
        return "neutral"
