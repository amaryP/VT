<!-- Use this file to provide workspace-specific custom instructions to Copilot. For more details, visit https://code.visualstudio.com/docs/copilot/copilot-customization#_use-a-githubcopilotinstructionsmd-file -->

# Instructions projet data_collect_signals
- Logging en français, niveau warning.
- Respecter la structure : src/, tests/, logs/, DATA/, doc/.
- Utiliser la connexion API comme dans les autres projets Python du workspace.
- Générer un README.md et une wishlist d'actifs.
- Respecter l'approche TDD (tests unitaires d'abord).
- Générer un script principal pour la collecte d'indicateurs via taapi.io (EMA10, EMA21, ADX14, ATR14, Bollinger upper/middle/lower).
- Générer un export CSV avec une colonne par indicateur, une ligne par bougie, pour l'intervalle 15m.
- Arrêt du script en cas d'erreur en phase de test.
- Générer la documentation dans doc/.
