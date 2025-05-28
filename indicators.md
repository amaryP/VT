# Glossaire des indicateurs techniques taapi.io

Ce glossaire recense l’ensemble des indicateurs techniques disponibles via l’API taapi.io (plus de 200). Pour chaque indicateur, sont listés : le nom, l’endpoint, une courte description, les paramètres principaux, et un lien vers la documentation officielle.

> **Remarque** : Pour la liste exhaustive et à jour, consultez https://taapi.io/indicators/.

---

## Exemples d’indicateurs (extrait)

### RSI (Relative Strength Index)
- **Endpoint** : `/rsi`
- **Description** : Mesure la force et la vitesse des mouvements de prix pour détecter les situations de surachat/survente.
- **Paramètres principaux** : `optInTimePeriod` (période, ex : 14)
- **Documentation** : https://taapi.io/indicators/rsi/

### EMA (Exponential Moving Average)
- **Endpoint** : `/ema`
- **Description** : Moyenne mobile exponentielle, plus réactive aux variations récentes que la SMA.
- **Paramètres principaux** : `optInTimePeriod` (période, ex : 9)
- **Documentation** : https://taapi.io/indicators/ema/

### MACD (Moving Average Convergence Divergence)
- **Endpoint** : `/macd`
- **Description** : Indicateur de tendance et de momentum basé sur la différence entre deux moyennes mobiles.
- **Paramètres principaux** : `optInFastPeriod`, `optInSlowPeriod`, `optInSignalPeriod`
- **Documentation** : https://taapi.io/indicators/macd/

### Bollinger Bands
- **Endpoint** : `/bbands`
- **Description** : Mesure la volatilité autour d’une moyenne mobile.
- **Paramètres principaux** : `optInTimePeriod`, `optInNbDevUp`, `optInNbDevDn`
- **Documentation** : https://taapi.io/indicators/bbands/

### 3 White Soldiers
- **Endpoint** : `/3whitesoldiers`
- **Description** : Détecte la figure de retournement haussier composée de trois bougies vertes consécutives.
- **Paramètres principaux** : Aucun spécifique (utilise les paramètres standards : exchange, symbol, interval)
- **Documentation** : https://taapi.io/indicators/3whitesoldiers/

### Doji
- **Endpoint** : `/doji`
- **Description** : Détecte la figure de retournement d’indécision (Doji) sur les chandeliers japonais.
- **Paramètres principaux** : Aucun spécifique
- **Documentation** : https://taapi.io/indicators/doji/

### Engulfing
- **Endpoint** : `/engulfing`
- **Description** : Détecte les patterns d’avalement haussier ou baissier.
- **Paramètres principaux** : Aucun spécifique
- **Documentation** : https://taapi.io/indicators/engulfing/

### SuperTrend
- **Endpoint** : `/supertrend`
- **Description** : Indicateur de tendance basé sur l’ATR.
- **Paramètres principaux** : `optInMultiplier`, `optInTimePeriod`
- **Documentation** : https://taapi.io/indicators/supertrend/

### Ichimoku
- **Endpoint** : `/ichimoku`
- **Description** : Système complet d’analyse de tendance, support/résistance et signaux d’achat/vente.
- **Paramètres principaux** : `optInTenkanSen`, `optInKijunSen`, `optInSenkouSpanB`
- **Documentation** : https://taapi.io/indicators/ichimoku/

### Liste complète
Pour la liste complète (plus de 200 indicateurs, dont tous les patterns de chandeliers, oscillateurs, moyennes mobiles, etc.), consultez : https://taapi.io/indicators/

Chaque page d’indicateur détaille :
- Les paramètres spécifiques
- Un exemple d’appel API
- Le format de la réponse
- Les cas d’usage

---

*Ce glossaire est un point d’entrée rapide pour l’exploration des indicateurs taapi.io. Pour l’automatisation ou l’intégration avancée, il est conseillé de parser dynamiquement la page officielle ou d’utiliser la documentation API.*
