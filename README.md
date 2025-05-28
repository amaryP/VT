# VT - Moteur de Trading Algorithmique

## Présentation
Ce projet est une infrastructure Python/PostgreSQL pour le développement, le test et l’industrialisation d’un moteur de trading algorithmique. Il intègre la récupération d’indicateurs techniques via l’API taapi.io, la journalisation en base et en logs, et la gestion avancée des tests unitaires et d’intégration.

## Fonctionnalités principales
- **Récupération d’indicateurs techniques** (RSI, Bollinger, EMA, etc.) via taapi.io
- **Gestion de l’intervalle** (ex : "1h", "15m") dans tous les modules, la base et les tests
- **Stockage des signaux bruts** dans PostgreSQL (table `signaux_bruts`)
- **Journalisation des résultats de tests** dans la table `testresult.resultats_tests` et dans un fichier log (`TEST/LOGS/test_results.log`)
- **Tests unitaires et d’intégration industrialisés**
- **Scripts d’intégration** pour valider la chaîne complète (API → analyse → log en base)

## Structure du projet
- `main_signal_to_db_1h.py` : Script principal d’intégration (récupère un signal, l’insère en base, logue le résultat)
- `main_signal_to_db_15m.py` : Variante pour l’intervalle "15m"
- `pg_logger.py` : Module d’insertion en base PostgreSQL
- `taapi_client.py` : Client API taapi.io (mode réel ou DRY_RUN)
- `TEST/` : Tests unitaires (insertion, lecture, présence clé API, etc.)
- `run_all_integration.ps1` : Script PowerShell pour exécuter tous les tests d’intégration
- `create_signaux_bruts.sql` : Script de création de la table `signaux_bruts`

## Variables d’environnement nécessaires
- `KEY_TAAPI_IO` : Clé API taapi.io (obligatoire sauf en DRY_RUN)
- Variables de connexion PostgreSQL (ex : `BDD_VT_PG_HOST_PC1000`, etc.)
- `DRY_RUN` : Mettre à "1" pour simuler les appels API (utile pour les tests sans consommer de quota)

## Exécution des scripts
- **Test d’intégration 1h** :
  ```powershell
  python main_signal_to_db_1h.py
  ```
- **Test d’intégration 15m** :
  ```powershell
  python main_signal_to_db_15m.py
  ```
- **Exécution automatisée de tous les tests d’intégration** :
  ```powershell
  .\run_all_integration.ps1
  ```

## Journalisation des résultats
- Tous les tests (unitaires et intégration) loguent leur résultat :
  - En base : table `testresult.resultats_tests`
  - En fichier : `TEST/LOGS/test_results.log`

## Gestion de l’intervalle
- L’intervalle (ex : "1h", "15m") est systématiquement transmis à l’API, stocké dans la base et vérifié dans les tests.
- La colonne `intervalle` est présente dans la table `signaux_bruts`.

## Mode DRY_RUN
- Pour tester la chaîne sans consommer d’appel API réel, lancer :
  ```powershell
  $env:DRY_RUN="1"; python main_signal_to_db_1h.py
  ```

## Dépannage
- **Erreur 429 (Too Many Requests)** : Utiliser le mode DRY_RUN ou attendre le reset du quota taapi.io.
- **Erreur de sérialisation JSON** : Le champ `raw_json` est automatiquement rendu compatible JSON.
- **Erreur de clé API** : Vérifier que `KEY_TAAPI_IO` est bien définie dans l’environnement.

## Documentation taapi.io
- [Documentation intégration taapi.io](https://taapi.io/documentation/integration/)
- [Liste des indicateurs disponibles](https://taapi.io/indicators/)

## Indicateurs techniques disponibles via taapi.io

Taapi.io propose de nombreux indicateurs techniques utilisables dans ce projet. Voici les principaux :

- **RSI (Relative Strength Index)** : Mesure la force et la vitesse des mouvements de prix. Utilisé pour détecter les situations de surachat/survente.
- **EMA (Exponential Moving Average)** : Moyenne mobile exponentielle, plus réactive aux variations récentes que la SMA.
- **SMA (Simple Moving Average)** : Moyenne mobile simple, lissée sur une période donnée.
- **MACD (Moving Average Convergence Divergence)** : Indicateur de tendance et de momentum basé sur la différence entre deux moyennes mobiles.
- **Bollinger Bands** : Mesure la volatilité autour d’une moyenne mobile, utile pour détecter les phases de compression/expansion.
- **Stochastic** : Indicateur de momentum comparant le cours de clôture à une plage de prix sur une période donnée.
- **ADX (Average Directional Index)** : Mesure la force d’une tendance, sans indiquer la direction.
- **CCI (Commodity Channel Index)** : Détecte les cycles de surachat/survente.
- **ATR (Average True Range)** : Mesure la volatilité du marché.
- **Ichimoku** : Système complet d’analyse de tendance, support/résistance et signaux d’achat/vente.
- **SuperTrend** : Indicateur de tendance basé sur l’ATR.
- **VWAP (Volume Weighted Average Price)** : Prix moyen pondéré par le volume, utilisé par les traders institutionnels.

Pour la liste exhaustive et les paramètres de chaque indicateur : [Liste officielle taapi.io](https://taapi.io/indicators/)

### Exemple d’appel API (RSI)
```json
{
  "indicator": "rsi",
  "exchange": "binance",
  "symbol": "BTC/USDT",
  "interval": "1h",
  "optInTimePeriod": 14
}
```

- **indicator** : nom de l’indicateur (voir la liste ci-dessus)
- **exchange** : plateforme (ex : binance, coinbase, etc.)
- **symbol** : paire de trading (ex : BTC/USDT)
- **interval** : timeframe (ex : 1h, 15m, 1d)
- **optInTimePeriod** : (optionnel) période de calcul (dépend de l’indicateur)

> Voir la documentation taapi.io pour les paramètres spécifiques à chaque indicateur.

## Endpoints API taapi.io – Liste exhaustive

Chaque indicateur taapi.io possède son propre endpoint HTTP. L’appel se fait en GET sur l’URL suivante :

```
https://api.taapi.io/<nom_indicateur>
```

Voici la liste exhaustive des endpoints principaux (au 25/05/2025) :

| Indicateur         | Endpoint                          |
|--------------------|-----------------------------------|
| RSI                | https://api.taapi.io/rsi          |
| EMA                | https://api.taapi.io/ema          |
| SMA                | https://api.taapi.io/sma          |
| MACD               | https://api.taapi.io/macd         |
| Bollinger Bands    | https://api.taapi.io/bbands       |
| Stochastic         | https://api.taapi.io/stoch        |
| ADX                | https://api.taapi.io/adx          |
| CCI                | https://api.taapi.io/cci          |
| ATR                | https://api.taapi.io/atr          |
| Ichimoku           | https://api.taapi.io/ichimoku     |
| SuperTrend         | https://api.taapi.io/supertrend   |
| VWAP               | https://api.taapi.io/vwap         |
| MFI                | https://api.taapi.io/mfi          |
| Williams %R        | https://api.taapi.io/willr        |
| Ultimate Oscillator| https://api.taapi.io/ultosc       |
| Parabolic SAR      | https://api.taapi.io/sar          |
| OBV                | https://api.taapi.io/obv          |
| MOM                | https://api.taapi.io/mom          |
| ROC                | https://api.taapi.io/roc          |
| TRIX               | https://api.taapi.io/trix         |
| Keltner Channels   | https://api.taapi.io/kc           |
| Donchian Channels  | https://api.taapi.io/donchian     |
| Envelope           | https://api.taapi.io/envelope     |
| PPO                | https://api.taapi.io/ppo          |
| PPO Histogram      | https://api.taapi.io/ppohistogram |
| PPO Signal         | https://api.taapi.io/pposignal    |
| DEMA               | https://api.taapi.io/dema         |
| TEMA               | https://api.taapi.io/tema         |
| WMA                | https://api.taapi.io/wma          |
| HMA                | https://api.taapi.io/hma          |
| ZLEMA              | https://api.taapi.io/zlema        |
| VWMA               | https://api.taapi.io/vwma         |
| Median Price       | https://api.taapi.io/medianprice  |
| Typical Price      | https://api.taapi.io/typicalprice |
| Weighted Close     | https://api.taapi.io/weightedclose|
| Price Oscillator   | https://api.taapi.io/priceosc     |
| ...                | ...                               |

> Pour la liste complète et à jour, consulter : https://taapi.io/indicators/

**Paramètres communs à tous les endpoints** :
- `secret` : clé API taapi.io (obligatoire)
- `exchange` : nom de la plateforme (ex : binance)
- `symbol` : paire de trading (ex : BTC/USDT)
- `interval` : timeframe (ex : 1h, 15m, 1d)
- Paramètres spécifiques selon l’indicateur (voir documentation taapi.io)

**Exemple d’appel complet (RSI)**
```
GET https://api.taapi.io/rsi?secret=VOTRE_CLE&exchange=binance&symbol=BTC/USDT&interval=1h&optInTimePeriod=14
```

> Chaque endpoint retourne un objet JSON contenant la valeur de l’indicateur et éventuellement des métadonnées.

---

*Pour toute question ou amélioration, ouvrir une issue ou contacter le mainteneur.*
