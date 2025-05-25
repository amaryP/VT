# 📘 Glossaire – Projet VT (Visual Trading Engine)

---

## 📌 A. Indicateurs techniques

| Terme              | Définition                                                            | Utilité principale                                                                          |
| ------------------ | --------------------------------------------------------------------- | ------------------------------------------------------------------------------------------- |
| **RSI**            | Relative Strength Index – mesure la force relative des hausses/baisse | Détecter surachat (>70) ou survente (<30)                                                   |
| **RSI5 / RSI14**   | RSI calculé sur 5 ou 14 périodes                                      | RSI5 = rapide / RSI14 = plus lisse                                                          |
| **MACD**           | EMA(12) - EMA(26)                                                     | Indique le momentum                                                                         |
| **MACD Signal**    | EMA(9) de la ligne MACD                                               | Sert à détecter des croisements                                                             |
| **MACD Histogram** | MACD - Signal                                                         | Montre la force du momentum                                                                 |
| **EMA**            | Moyenne mobile exponentielle                                          | Tendance de fond, plus réactive qu'une SMA                                                  |
| **BBands**         | Bandes de Bollinger : BBUpper, BBMid, BBLower                         | Détecte les extrêmes et la volatilité                                                       |
| **Volume relatif** | Volume actuel / volume moyen sur n bougies                            | Détecte les pics d’activité réels, filtre les actifs peu liquides (seuil de 1.5 recommandé) |
| **Close**          | Valeur de clôture de la dernière bougie selon l'intervalle choisi     | Sert à positionner le prix dans les bandes ou à confirmer une figure                        |
| **Open**           | Prix d'ouverture de la bougie analysée                                | Sert à calculer le corps de la bougie                                                       |
| **High**           | Prix le plus haut atteint pendant la bougie                           | Utilisé pour mesurer la volatilité ou une mèche haute                                       |
| **Low**            | Prix le plus bas atteint pendant la bougie                            | Utilisé pour mesurer la volatilité ou une mèche basse                                       |

---

## 📌 B. Patterns de chandeliers (figures techniques)

| Terme                 | Définition                                                  | Interprétation                       |
| --------------------- | ----------------------------------------------------------- | ------------------------------------ |
| **Hammer**            | Petite bougie avec longue mèche basse                       | Retournement haussier probable       |
| **Inverted Hammer**   | Mèche haute, corps bas, après tendance baissière            | Retournement haussier modéré         |
| **Morning Star**      | 3 bougies : rouge, doji, verte                              | Fort signal de retournement haussier |
| **Bullish Engulfing** | Bougie verte englobant entièrement la rouge précédente      | Signal haussier fort                 |
| **Piercing Line**     | Ouverture en gap bas + clôture > 50% de la rouge précédente | Signal haussier modéré               |

---

## 📌 C. Intervalles (timeframes)

| Intervalle | Signification         | Usage                           |
| ---------- | --------------------- | ------------------------------- |
| `5m`       | 5 minutes par bougie  | Scalping, très réactif/bruité   |
| `15m`      | 15 minutes par bougie | Intraday actif, bon compromis ✅ |
| `1h`       | 1 heure par bougie    | Moyen terme, plus stable        |
| `1d`       | 1 jour par bougie     | Swing trading, analyse macro    |

---

## 📌 D. Logique du moteur (VT)

| Terme                 | Rôle                                                      | Exemple                                         |
| --------------------- | --------------------------------------------------------- | ----------------------------------------------- |
| **Étape 1**           | Vérification du contexte de marché                        | RSI14 < 30, RSI5 > RSI14, Close < BBMid         |
| **Étape 2**           | Recherche de figure si le contexte est favorable          | Pattern = "hammer"                              |
| **signaux\_bruts**    | Table des données API complètes stockées à chaque appel   | Tous les indicateurs                            |
| **evenementset**      | Table des signaux filtrés valides                         | Uniquement si Étape 1 + Étape 2 sont OK         |
| **DRY\_RUN**          | Mode de test sans passage d’ordre réel                    | Permet de tester en conditions sûres            |
| **method\_code**      | Code ou ID d’une stratégie utilisée                       | Ex : "RSI30\_BB\_HAMMER"                        |
| **Méthode d’entrée**  | Condition définissant le moment d’entrée en position      | Pattern haussier détecté + volume relatif élevé |
| **Méthode de suivi**  | Stratégie de gestion active une fois la position ouverte  | Stop suiveur avec marge de respiration variable |
| **Méthode de sortie** | Critère ou scénario déclenchant la clôture de la position | Take profit, stop loss ou signal opposé détecté |
