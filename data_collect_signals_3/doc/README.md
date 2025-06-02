# Documentation projet data_collect_signals

## Objectif
Collecter automatiquement les indicateurs EMA10, EMA21, ADX14, ATR14, Bollinger upper/middle/lower via l’API taapi.io pour un actif unique (crypto ou action), intervalle 15m, et stocker les résultats dans un CSV.

## Indicateurs collectés
- ema_10
- ema_21
- adx_14
- atr_14
- bollinger_upper
- bollinger_middle
- bollinger_lower

## Structure du projet
- src/ : code source
- tests/ : tests unitaires
- logs/ : logs d’exécution/tests
- DATA/ : CSV générés, wishlist
- doc/ : documentation

## Approche TDD
- Écriture des tests unitaires avant l’implémentation fonctionnelle.

## Logging
- Niveau : warning
- Langue : français

## Gestion des erreurs
- Arrêt du script en cas d’erreur en phase de test.

## Format CSV
- Une colonne par indicateur, une ligne par bougie (15m), nommage : actif_intervalle_datetime.csv

## Connexion API
- Utilise la clé API TAAPI.IO comme dans les autres projets Python du workspace (variable d’environnement KEY_TAAPI_IO).
