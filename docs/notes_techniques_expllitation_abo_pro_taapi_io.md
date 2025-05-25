# 📘 Notes techniques – Exploitation de l'abonnement Taapi.io Pro

## 🎯 Objectif

Tirer le meilleur parti de l'abonnement "Pro" Taapi.io (14,99 €/mois) dans le cadre du projet VT, tout en maîtrisant la volumétrie des données et en optimisant les appels API.

---
Structure des appels Taapi.io – bulk & indicateurs
L'API bulk de Taapi.io permet d’interroger jusqu’à 3 symboles en une seule requête, mais chaque symbole ne peut être associé qu’à un seul indicateur dans cet appel. À l’inverse, l’API standard (non-bulk) permet de demander jusqu’à 20 indicateurs différents pour un seul symbole.
👉 Ainsi, pour analyser 30 actifs avec 20 indicateurs chacun :

En bulk → 20 appels nécessaires (1 appel par indicateur × 3 symboles max par appel)

En standard → 30 appels nécessaires (1 appel par symbole avec tous les indicateurs d’un coup)

Ce comportement oriente les choix de conception entre performance, simplicité, et volumétrie API.




## ✅ Capacités de l'offre "Pro" Taapi.io

| Fonctionnalité                   | Utilisation recommandée dans VT                                            |
| -------------------------------- | -------------------------------------------------------------------------- |
| **150 000 appels API / jour**    | Suivi d’un grand nombre d’actifs (crypto et actions US), intervalle 15 min |
| **3 symboles par appel bulk**    | Réduction du volume d'appels, parfaite pour `signaux_bruts`                |
| **Crypto + US Stocks + Indexes** | Intégration de BTC, ETH, SOL, mais aussi SPY, QQQ, AAPL, TSLA              |
| **300 chandeliers historiques**  | Backtest glissant, calcul de moyennes, détection de schémas récents        |
| **Support prioritaire**          | Utilisable en cas de blocage technique API ou limites détectées            |

---

## 📊 Exploitation des 300 bougies historiques

### 📥 Récupération des indicateurs historiques

* L'abonnement permet de récupérer **les valeurs numériques des indicateurs techniques** pour chaque bougie :

  * RSI, EMA, MACD, Bollinger (BBUpper, BBMid, BBLower), Volume, Open, High, Low, Close
* Cela permet de **reconstituer le contexte technique réel** de chaque bougie passée, indispensable pour les backtests sérieux
* Les figures (hammer, etc.) peuvent aussi être croisées avec ces données pour **valider la qualité du signal dans son contexte**

### 1. Calcul des **moyennes de volume**

* Permet de déterminer le **volume moyen sur les 20 ou 50 dernières bougies**
* Sert de base pour le **volume relatif** (ex : `volume / moyenne_volume`) avec seuil par défaut à `1.5`

### 2. Détection de **divergences RSI**

* Identifier des configurations où le **prix baisse mais le RSI monte** (ou l'inverse)
* Utile pour anticiper des retournements même **sans figure de chandeliers apparente**

### 3. Préparation d’un **backtest glissant**

* Application rétroactive d’une règle sur les **300 dernières bougies (≈3 jours en 15m)**
* Permet de valider des stratégies simples sans moteur de backtest complexe

Exemple :

```python
for candle in last_300_candles:
    if passes_conditions(candle):
        log_signal(candle)
```

---

## 📈 Qu'est-ce que l'index SPY ?

**SPY** est un **ETF (Exchange-Traded Fund)** qui réplique la performance de l’indice **S\&P 500**, c’est-à-dire les 500 plus grandes entreprises cotées aux États-Unis.

| Élément         | Détail                                           |
| --------------- | ------------------------------------------------ |
| **Nom complet** | SPDR S\&P 500 ETF Trust                          |
| **Ticker**      | SPY                                              |
| **Bourse**      | NYSE Arca                                        |
| **Type**        | ETF (fonds indiciel coté)                        |
| **Représente**  | L’indice S\&P 500 (actions américaines majeures) |

### 📌 Pourquoi l’utiliser ?

* C’est un **baromètre du marché US** : quand SPY chute fortement, cela reflète un stress général.
* Il est donc **utile comme filtre macro** : si SPY est en tendance baissière ou en fort recul, tu peux décider de **ne pas initier de position**, même si un signal individuel est bon (crypto, action...).

### ✅ Dans VT :

* On peut récupérer SPY via Taapi.io comme n’importe quel symbole (ex : `SPY/US`) et l’ajouter à l’analyse de contexte.
* Exemple : si `RSI14(SPY) < 40` ou `close < EMA(SPY)`, on inhibe certaines stratégies.

---

## 🧠 Conseils pratiques

* **Limiter la profondeur d’historique stockée en base** : 30 à 60 jours max, ou stockage parallèle (archive)
* **Ne pas lancer d’appel de pattern** si les indicateurs de contexte (RSI/BB/volume) ne valident pas l’analyse
* **Utiliser les index SPY/QQQ/DJIA** comme filtre macro global (ex : éviter les achats crypto si SPY décroche)
* **Garder une trace des méthodes testées**, y compris les paramètres (intervalle, stop, TP, pattern)

---

Ce fichier pourra évoluer à mesure que les optimisations seront testées.
