### Glossaire des indicateurs pour le suivi de tendance intraday (day trading)

#### 🔄 Intervalle vs Période

* **Intervalle (`interval`)** : la granularité temporelle des bougies. Ex. : `1m`, `5m`, `15m`, `1h`, `1d`.
* **Période (`optInTimePeriod`)** : le nombre de bougies prises en compte dans le calcul de l'indicateur. Ex. : `10`, `14`, `20`.

Exemple : une EMA10 sur un intervalle 15m prend en compte **les 10 dernières bougies de 15 minutes** → soit 150 minutes.

---

| Indicateur              | Définition technique                                           | Utilité métier                                                        | Formule de calcul                                                                                                                             | Disponible via TAAPI.IO | Paramètre requis (ex. période)            |         |                   |     |                       |
| ----------------------- | -------------------------------------------------------------- | --------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------- | ----------------------- | ----------------------------------------- | ------- | ----------------- | --- | --------------------- |
| **EMA**                 | Moyenne mobile exponentielle, plus sensible aux récents prix.  | Détecte les changements de tendance via croisements EMA courts/longs. | EMA(t) = α × Prix(t) + (1 − α) × EMA(t−1), avec α = 2 / (n + 1)                                                                               | Oui                     | Oui (optInTimePeriod)                     |         |                   |     |                       |
| **ADX**                 | Mesure la force d'une tendance, sans direction.                | Identifie si le marché est en tendance (ADX > 25) ou en range.        | Moyenne des DI+ et DI- sur n périodes.                                                                                                        | Oui                     | Oui (optInTimePeriod)                     |         |                   |     |                       |
| **ATR**                 | Indique la volatilité en moyenne sur une période donnée.       | Utilisé pour positionner des stop-loss adaptés à la volatilité.       | Moyenne mobile du True Range (TR) : max(H-L,                                                                                                  | H-Cpréc                 | ,                                         | L-Cpréc | ) sur n périodes. | Oui | Oui (optInTimePeriod) |
| **Supertrend**          | Indicateur de tendance basé sur l'ATR.                         | Fournit des signaux visuels clairs de tendance (achat/vente).         | Supertrend = (H + L)/2 ± multiplier × ATR                                                                                                     | Oui                     | Oui (ATR period + multiplier)             |         |                   |     |                       |
| **Bandes de Bollinger** | Mesure la volatilité autour d'une moyenne mobile simple (SMA). | Identifie les états de surachat/survente et les cassures.             | SMA ± k × écart-type sur n périodes. L'API retourne trois valeurs : `valueUpperBand`, `valueMiddleBand`, `valueLowerBand` dans un seul appel. | Oui                     | Oui (optInTimePeriod, stdDevUp, stdDevDn) |         |                   |     |                       |
| **Volume**              | Volume d'actifs échangés pendant une unité de temps.           | Confirme la force d'un mouvement ou signal de cassure.                | Somme des volumes échangés.                                                                                                                   | Oui                     | Non                                       |         |                   |     |                       |

---

### 🧾 Champs nécessaires pour stocker les indicateurs calculés (base de données ou fichier)

| Champ          | Description                                                                          | Exemple                |
| -------------- | ------------------------------------------------------------------------------------ | ---------------------- |
| **symbole**    | Le symbole de l’actif                                                                | `BTC/USDT`             |
| **intervalle** | Granularité des bougies utilisées                                                    | `15m`                  |
| **indicateur** | Nom de l’indicateur                                                                  | `EMA`                  |
| **période**    | Période utilisée dans le calcul                                                      | `10`, `14`...          |
| **datetime**   | Date et heure de la valeur calculée                                                  | `2025-05-27T12:15:00Z` |
| **valeur**     | Valeur de l’indicateur à cet instant                                                 | `27250.34`             |
| **open**       | (optionnel, à récupérer seulement si signal pertinent) Prix d’ouverture de la bougie | `27120.00`             |
| **close**      | (optionnel, à récupérer seulement si signal pertinent) Prix de clôture de la bougie  | `27250.34`             |
| **high**       | (optionnel) Prix le plus haut de la bougie                                           | `27300.00`             |
| **low**        | (optionnel) Prix le plus bas de la bougie                                            | `27090.00`             |
| **volume**     | (optionnel) Volume échangé durant la bougie                                          | `1342.50`              |

🔎 **Remarque** : pour une stratégie complète (entrée, suivi, sortie), les champs comme `close`, `high`, `low`, `volume` sont utiles pour détecter les breakouts, fixer des stops dynamiques (ex. via ATR), ou valider les signaux avec le volume.

💡 **Optimisation** : pour économiser les appels API, on peut d'abord scanner les signaux de base (EMA/ADX), et ne récupérer les informations complémentaires (`close`, `volume`, etc.) **qu'en cas de signal d'entrée potentiel**.

---

### 📦 Appel API en phase 1 : combien de paramètres nécessaires ?

Pour détecter un signal d’entrée avec les indicateurs EMA et ADX sur un actif donné via l’endpoint `/bulk`, voici les paramètres à passer :

| Paramètre         | Description                                 | Exemple      |
| ----------------- | ------------------------------------------- | ------------ |
| `symbol`          | Actif analysé                               | `BTC/USDT`   |
| `interval`        | Timeframe                                   | `15m`        |
| `indicator`       | Type d’indicateur                           | `ema`, `adx` |
| `optInTimePeriod` | Période de calcul spécifique par indicateur | `10`, `14`   |

➡️ Cela donne **6 paramètres distincts** pour analyser un actif avec EMA + ADX.

🧮 **Limite TAAPI.IO gratuite : 20 paramètres par appel** → tu peux donc traiter **3 actifs par appel** (3 × 6 = 18), ce qui reste **sous la limite** et t’évite de multiplier les requêtes.

---

### 📊 Exemples de formules de détection de tendance (utilisables ligne par ligne)

> Dans un fichier CSV ou DataFrame, chaque ligne contient tous les éléments pour appliquer ces filtres **en une seule passe**. Les stratégies éprouvées utilisent souvent plusieurs conditions combinées (multi-filtrage).

| Nom de la stratégie             | Formule                                                             | But recherché                       |
| ------------------------------- | ------------------------------------------------------------------- | ----------------------------------- |
| **Tendance haussière simple**   | `(ema_10 > ema_21) and (adx_14 > 25)`                               | Confirme une tendance avec sa force |
| **Tendance baissière simple**   | `(ema_10 < ema_21) and (adx_14 > 25)`                               | Détecte une baisse soutenue         |
| **Breakout haussier Bollinger** | `(close > valueUpperBand) and (adx_14 > 25)`                        | Cassure + force                     |
| **Breakout baissier Bollinger** | `(close < valueLowerBand) and (adx_14 > 25)`                        | Cassure vers le bas confirmée       |
| **Croisement + Volume**         | `(ema_10 > ema_21) and (adx_14 > 25) and (volume > mean_volume_20)` | Ajout de filtre de liquidité        |

🔁 Ces formules peuvent servir de **base pour tester différentes variantes** de stratégie avant industrialisation.

---

### 🧩 Structure typique d'une stratégie éprouvée : filtres combinés

Les stratégies robustes de suivi de tendance appliquent plusieurs **filtres successifs** pour valider une position. Chaque filtre élimine les cas douteux ou faiblement corrélés à une tendance réelle.

| Étape | Filtre                              | Objectif visé                              |
| ----- | ----------------------------------- | ------------------------------------------ |
| 1     | EMA courte > EMA longue             | Détecter une dynamique directionnelle      |
| 2     | ADX > 25                            | Confirmer la force de la tendance          |
| 3     | Prix > bande supérieure (Bollinger) | Valider la cassure d’un range              |
| 4     | Volume > moyenne mobile du volume   | Filtrer les mouvements sans engagement     |
| 5     | RSI pas en surachat (optionnel)     | Éviter les tops potentiels en fin de cycle |

💡 Un système de test modulaire peut appliquer un ou plusieurs de ces filtres dynamiquement, permettant d’évaluer leur pertinence et leur impact combiné.
