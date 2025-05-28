# data_collect_signals

Projet Python pour la collecte automatisée d'indicateurs de tendance via l'API taapi.io.

## Fonctionnalités
- Récupération EMA10, EMA21, ADX14, ATR14, ATR14, Bollinger upper/middle/lower pour un actif unique (crypto ou action).
- Intervalle 15m (modifiable).
- Export CSV (une ligne par bougie, une colonne par indicateur, nommage : actif_intervalle_datetime.csv).
- Logging en français (niveau warning).
- Structure : src/, tests/, logs/, DATA/, doc/.
- Wishlist d'actifs dans DATA/.
- TDD : tests unitaires pour chaque fonction.

## Lancer le projet
1. Installer les dépendances :
   ```powershell
   pip install -r requirements.txt
   ```
2. Renseigner la clé API TAAPI.IO dans la variable d'environnement KEY_TAAPI_IO.
3. Lancer le script principal dans src/.

## Structure du projet
- src/ : code source Python
- tests/ : tests unitaires (pytest)
- logs/ : logs d'exécution et de tests
- DATA/ : fichiers CSV générés, wishlist d'actifs
- doc/ : documentation technique et fonctionnelle

## Approche TDD
- Les tests unitaires sont à écrire et à valider avant toute implémentation fonctionnelle.

## Licence
Projet privé, usage personnel.
