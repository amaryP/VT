# Questions sur l'alignement des timestamps des bougies US stocks via taapi.io

- Pourquoi les timestamps retournés par l’API taapi.io pour les actions US (ex : Nasdaq) avec interval=15m ne sont-ils pas alignés sur les quarts d’heure "ronds" (ex : 16:43 au lieu de 16:45) ?
- Existe-t-il un autre champ temporel dans la réponse API (ex : timestamp, timestampHuman, candle, etc.) qui donnerait une information plus fiable ou explicite sur la bougie ?
- Ce comportement est-il propre à taapi.io ou au provider Polygon pour les actions US ?
- Comment Polygon détermine-t-il le timestamp de clôture d’une bougie 15m pour les US stocks ?
- Peut-on forcer l’alignement des timestamps sur les quarts d’heure via un paramètre ou une option ?
- Quelle est la meilleure pratique pour fiabiliser la correspondance entre l’heure système, l’intervalle demandé et le timestamp de la bougie close retournée ?

Contexte :
- Collecte automatisée via taapi.io en bulk sur tous les actifs du Nasdaq, intervalle 15m, stockage CSV.
- Les timestamps dans le CSV ne sont pas alignés sur les multiples de 15 minutes.
- Diagnostic et recherche documentaire en cours.
