# 📘 Stratégies de trading basées sur l'analyse graphique (VT)

Ce document présente 5 stratégies modernes de trading graphique utilisées dans le projet VT, avec les conditions techniques détaillées, les règles d'entrée et de sortie, et les champs nécessaires pour les implémenter en base de données ou dans les fonctions Python.

---

## 🎯 Stratégie 1 : Pullback RSI + EMA (tendance haussière)

**Intervalle** : 15 min
**Objectif** : détecter un repli temporaire dans une tendance forte

**Conditions d'entrée** :

* RSI14 entre 30 et 45
* Close >= EMA50
* EMA20 > EMA50 (tendance haussière)
* Volume relatif >= 1.3
* Contexte SPY : haussier ou neutre

**Signal** : Achat
**TP** : +2.5%
**SL** : -1.2%
**Durée max** : 2h

---

## 🚀 Stratégie 2 : Breakout + volume

**Intervalle** : 5 min
**Objectif** : capter les départs de mouvements puissants

**Conditions d'entrée** :

* Close > BB Upper + 0.3%
* RSI5 entre 65 et 80
* Volume relatif > 2
* MACD histogram > 0

**Signal** : Achat
**TP** : +4%
**SL** : -2%
**Trailing SL** activé après +2%

---

## 🧠 Stratégie 3 : Divergence RSI + figure de retournement

**Intervalle** : 30 min
**Objectif** : détecter un retournement sur excès vendeur

**Conditions d'entrée** :

* Divergence haussière RSI (prix plus bas mais RSI plus haut)
* Pattern : inverted hammer ou bullish engulfing
* RSI14 < 40
* Close < BB Mid
* Volume relatif >= 1.2
* Contexte SPY : neutre ou en rebond

**Signal** : Achat
**TP** : +3%
**SL** : -1.5%
**Durée max** : 3h

---

## 📉 Stratégie 4 : Vente sur surachat + cassure

**Intervalle** : 15 min
**Objectif** : détecter des retournements à la baisse après excès

**Conditions d'entrée** :

* RSI14 > 75
* Close < EMA20
* Pattern : bearish engulfing ou evening star
* Volume relatif > 1.8
* Contexte SPY : baissier

**Signal** : Vente
**TP** : +2.5%
**SL** : -1%
**Trailing SL** : dès +1% de gain

---

## 🔁 Stratégie 5 : Compression + cassure de range

**Intervalle** : 1h
**Objectif** : profiter d'une sortie de range après resserrement

**Conditions d'entrée** :

* BB width < 2%
* Volume relatif moyen (6 dernières bougies) < 0.8
* EMA20 ≈ EMA50 (écart < 1%)
* Pattern : doji ou spinning top

**Déclenchement** :

* Si cassure BB Upper → Achat
* Si cassure BB Lower → Vente

**TP** : +3%
**SL** : -1.5%
**Durée max** : 4h

---

## 📊 Colonnes nécessaires en base (ex. signaux\_bruts)

```sql
id SERIAL PRIMARY KEY,
symbol VARCHAR,
dateheure TIMESTAMP,
intervalle VARCHAR,
close NUMERIC,
open NUMERIC,
high NUMERIC,
low NUMERIC,
volume NUMERIC,
volume_moy20 NUMERIC,
volume_relatif NUMERIC,
volume_relatif_moy6 NUMERIC,
ema20 NUMERIC,
ema50 NUMERIC,
rsi5 NUMERIC,
rsi14 NUMERIC,
bb_upper NUMERIC,
bb_lower NUMERIC,
bb_mid NUMERIC,
bb_width NUMERIC,
macd_histogram NUMERIC,
divergence_rsi BOOLEAN,
pattern VARCHAR,
context_spy VARCHAR,
strategy_match TEXT[]
```

Chaque stratégie détectée pourra être ajoutée dans la colonne `strategy_match` (liste ou jsonb).
