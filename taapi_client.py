import os
import requests
import logging

class TaapiClient:
    def __init__(self, dry_run: bool = False):
        self.api_key = os.getenv("TAAPI_API_KEY")
        self.base_url = "https://api.taapi.io"
        self.dry_run = dry_run
        if not self.api_key and not self.dry_run:
            raise ValueError("TAAPI_API_KEY non trouvée dans l'environnement")

    def get_indicators(self, symbol: str, interval: str = "1h"):
        if self.dry_run:
            logging.info(f"[DRY_RUN] Simulation d'appel API pour {symbol} {interval}")
            # Exemple de données simulées
            return {
                "symbol": symbol,
                "interval": interval,
                "rsi14": 28.5,
                "rsi5": 33.1,
                "bb_upper": 30000.0,
                "bb_lower": 25000.0,
                "bb_mid": 27500.0,
                "ema": 27000.0,
                "close": 27200.0,
                "open": 27100.0,
                "high": 27300.0,
                "low": 27000.0,
                "pattern": "hammer",
                "raw_json": {"simu": True}
            }
        endpoint = f"{self.base_url}/rsi"
        params = {
            "secret": self.api_key,
            "exchange": "binance",
            "symbol": symbol,
            "interval": interval,
            "optInTimePeriod": 14
        }
        response = requests.get(endpoint, params=params)
        response.raise_for_status()
        data = response.json()
        # À compléter pour parser tous les indicateurs nécessaires
        return data
