# Documentation technique et fonctionnelle du projet data_collect_signals

## Structure du projet
- src/ : code source
- tests/ : tests unitaires
- logs/ : logs d’exécution/tests
- DATA/ : CSV générés, wishlist
- doc/ : documentation

## Indicateurs collectés
- ema_10
- ema_21
- adx_14
- atr_14
- bollinger_upper
- bollinger_middle
- bollinger_lower

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
