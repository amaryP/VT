# DATA_PUMP_OHLCV

Outil de récupération OHLCV depuis TAAPI.IO, structuration et export CSV pour backtest.

## Lancer l’extraction

```bash
cd data_pump_ohlcv
python src/main_pump.py
```

## Lancer les tests

```bash
pytest
```

- Les données sont exportées dans `data/raw/crypto/BINANCE/`.
- Le script utilise la clé API TAAPI.IO présente dans les variables d’environnement.
