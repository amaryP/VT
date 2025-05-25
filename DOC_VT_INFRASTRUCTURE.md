# Documentation du projet VT – Infrastructure de tests et simulation pour moteur de trading algorithmique

## Objectif
Mettre en place une infrastructure robuste pour le développement, le test et l’intégration d’un moteur de trading algorithmique, avec journalisation avancée, gestion des intervalles, intégration d’API d’indicateurs techniques (taapi.io), et traçabilité complète des tests.

---

## Architecture générale

- **Base de données PostgreSQL** :
  - Tables principales :
    - `evenementset` : journalisation des événements.
    - `signaux_bruts` : stockage des signaux bruts récupérés/analytiques (avec colonnes `valeur` et `intervalle`).
    - `testresult.resultats_tests` : journalisation des résultats de tests (nom, statut, détails).
  - Scripts SQL pour création de tables et gestion des droits (`grant_all_rights_all_tables.sql`, `grant_testresult_rights.sql`).

- **Modules Python** :
  - `taapi_client.py` : client pour l’API taapi.io, gestion DRY_RUN, gestion de l’intervalle, récupération d’indicateurs (RSI, BB, EMA, etc.).
  - `pg_logger.py` : insertion et lecture dans les tables PostgreSQL, gestion de la connexion, log des signaux bruts et événements.
  - `main_signal_to_db_1h.py` / `main_signal_to_db_15m.py` : scripts d’intégration orchestrant la récupération d’indicateurs, la construction du signal, et la journalisation en base.
  - `test_*` : scripts de tests unitaires pour insertion/lecture, présence de la clé API, etc.
  - `run_all_integration.ps1` : exécution automatisée de tous les tests d’intégration.
  - `register_taapi_key.ps1` : enregistrement de la clé API taapi.io dans les variables d’environnement.

- **Logs** :
  - Fichier de logs dédié : `TEST/LOGS/test_results.log` (journalisation des résultats de tests et d’intégration).

---

## Fonctionnalités principales

- **Gestion des intervalles** :
  - Tous les modules et scripts gèrent dynamiquement l’intervalle (ex : "1h", "15m") pour la récupération et la journalisation des signaux.

- **Intégration API taapi.io** :
  - Récupération d’indicateurs techniques via l’API (clé stockée dans la variable d’environnement `KEY_TAAPI_IO`).
  - Mode DRY_RUN pour simuler les appels API lors des tests ou en cas d’erreur.

- **Journalisation avancée** :
  - Résultats de tests et d’intégration loggés à la fois en base (`testresult.resultats_tests`) et dans un fichier log.
  - Correction de la sérialisation JSON pour le champ `raw_json` (conversion récursive des datetime).

- **Tests unitaires et d’intégration** :
  - Tests pour insertion/lecture dans les tables, présence de la clé API, gestion de l’intervalle, etc.
  - Nettoyage automatique des données de test après exécution.
  - Log systématique des résultats de tests en base et en fichier.

- **Sécurité et gestion des droits** :
  - Scripts SQL pour accorder tous les droits nécessaires à l’utilisateur `vt_base_user`.

---

## Exécution

1. **Pré-requis** :
   - Python 3.10+
   - PostgreSQL
   - Clé API taapi.io (à enregistrer via `register_taapi_key.ps1`)
   - Variables d’environnement pour la connexion à la base (voir README.md)

2. **Tests unitaires** :
   - Lancer les scripts de test dans le dossier `TEST/`.
   - Les résultats sont loggés en base et dans `TEST/LOGS/test_results.log`.

3. **Intégration** :
   - Exécuter `main_signal_to_db_1h.py` ou `main_signal_to_db_15m.py` pour tester la chaîne complète (API → analyse → log en base).
   - Utiliser `run_all_integration.ps1` pour lancer tous les tests d’intégration.

---

## Variables d’environnement

- `KEY_TAAPI_IO` : clé API taapi.io
- `DRY_RUN` : "1" pour activer le mode simulation (pas d’appel réel à l’API)
- Variables de connexion PostgreSQL (ex : `BDD_VT_PG_HOST_PC1000`, `BDD_VT_PG_USER_VT_PC1000`, etc.)

---

## Liens utiles

- [Documentation taapi.io](https://taapi.io/documentation/)
- [Exemples d’intégration taapi.io](https://taapi.io/docs/)

---

## Extensions et améliorations possibles

- Automatisation CI/CD (GitHub Actions)
- Génération automatique de docstring
- Extension à d’autres intervalles/indicateurs
- Exemples d’utilisation avancée (bulk, manuel)
- Amélioration de la gestion des erreurs API (fallback DRY_RUN automatique)

---

## Auteurs
- Projet VT – 2024-2025
- Contact : [à compléter]

---

## Historique des modifications
- Voir le fichier `README.md` pour le changelog détaillé.
