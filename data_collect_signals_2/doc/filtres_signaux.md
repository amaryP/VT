### Filtres de détection de tendance - Suivi de tendance intraday

Voici un récapitulatif des filtres à implémenter dans un script Python pour détecter les opportunités de trading basées sur le suivi de tendance.

---

#### 1. **FilterCroisementEMA** (Croisement de moyennes mobiles : EMA10 > EMA21)

* **Objectif** : Identifier la direction du mouvement.
* **Formule** :

```python
filter_croisement_ema = ema_10 > ema_21
```

* **Interprétation** : Si la moyenne mobile courte (10 périodes) est supérieure à la longue (21), cela suggère un mouvement haussier.

---

#### 2. **FilterForceTendanceADX** (Force de la tendance : ADX > 25)

* **Objectif** : Confirmer qu’il y a bien une tendance (haussière ou baissière).
* **Formule** :

```python
filter_force_tendance_adx = adx_14 > 25
```

* **Interprétation** : Une valeur d’ADX supérieure à 25 indique une tendance suffisamment forte.

---

#### 3. **FilterBreakoutBollinger** (Cassure des bandes de Bollinger)

* **Objectif** : Détecter un breakout potentiellement rentable.
* **Formule (cassure haussière)** :

```python
filter_breakout_bollinger = close > bollinger_upper
```

* **Interprétation** : Si le prix de clôture dépasse la bande supérieure, on peut avoir une cassure haussière avec continuation.

---

#### 4. **FilterVolumeSignificatif** (Volume > moyenne mobile)

* **Objectif** : Valider le mouvement avec du volume (engagement du marché).
* **Formule** :

```python
filter_volume_significatif = volume > sma_volume_20
```

* **Interprétation** : Le volume est plus élevé que sa moyenne mobile (20 périodes), ce qui donne du poids au mouvement.

---

#### 5. **FilterRSIStable** (RSI < 70)

* **Objectif** : Éviter de trader en fin de cycle haussier.
* **Formule** :

```python
filter_rsi_stable = rsi_14 < 70
```

* **Interprétation** : Le RSI est inférieur à 70, on n'est donc pas encore en zone de surchauffe.

---

#### 6. **FilterVolatiliteATR** (Volatilité suffisante)

* **Objectif** : Écarter les actifs sans mouvement significatif.
* **Formule** :

```python
filter_volatilite_atr = abs(close - open) > (1.5 * atr_14)
```

* **Interprétation** : La variation intra-bougie est significative comparée à la volatilité moyenne (ATR).

---

#### 7. **FilterSignalCombine** (Filtre final combiné)

* **Formule** :

```python
signal = (
    filter_croisement_ema and
    filter_force_tendance_adx and
    filter_breakout_bollinger and
    filter_volume_significatif and
    filter_rsi_stable and
    filter_volatilite_atr
)
```

* **But** : Tous les filtres doivent être vrais pour considérer qu’un signal est valide.

---

#### 8. **StrategyCoreTrendOnly** (Stratégie partielle de suivi de tendance)

* **Objectif** : Détecter les situations de tendance forte et claire, sans exiger de breakout ni de volume exceptionnel.
* **Formule** :

```python
signal_core = (
    filter_croisement_ema and
    filter_force_tendance_adx and
    filter_volatilite_atr
)
```

* **Interprétation** :
  - Le croisement EMA valide la direction du mouvement.
  - L’ADX confirme la force de la tendance.
  - L’ATR garantit une volatilité suffisante.
* **Utilisation** :
  - Ce filtre partiel permet d’identifier les actifs en tendance nette, même sans breakout ni volume exceptionnel.
  - Peut servir de base à des stratégies plus avancées ou à des alertes préliminaires.

---

> Ces filtres peuvent être codés en Python pour analyser des fichiers CSV ou DataFrame contenant les indicateurs préalablement récupérés via l'API (ex: Taapi.io).
