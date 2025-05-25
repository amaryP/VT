📌 Projet : VT (Visual Trading Engine)
🎯 Objectif
Construire un moteur de trading algorithmique modulaire et automatisé, basé sur :

L’analyse technique (RSI, bandes de Bollinger, chandeliers)

La détection de signaux remarquables

L’enregistrement en base PostgreSQL

Une possible exécution future via API Interactive Brokers

🔁 Workflow général
Watchlist :

Liste fixe d’actifs à surveiller (BTC/USDT, ETH/USDT, etc.)

Récupération des indicateurs techniques :

Via taapi.io, en bulk si possible

RSI, BB, EMA

Filtrage primaire :

ex : RSI14 < 30, close < BBmid, RSI5 > RSI14

Détection facultative de patterns :

ex : marteau, morning star

Log en base PostgreSQL (evenementset)

Avec champs : symbol, dateheure, valeur, rsi14, rsi5, figure, eventlog

(À venir) : passage d’ordres réels (via IBKR), suivi des performances, auto-stop

🧪 Tests de non-régression
Chaque signal loggé avec les mêmes données doit produire exactement la même ligne

Aucun appel à l'API ne doit être fait si les conditions primaires ne sont pas réunies

Chaque module (logger, client, scanner) doit avoir des tests unitaires simples

Un mode DRY_RUN = True doit être activable pour simuler les appels

🧩 Modules attendus
taapi_client.py : accès aux indicateurs via API (bulk + pattern)

pg_logger.py : insertion en base PostgreSQL

main_logger.py : test d’un signal simulé

scanner.py : boucle de scan périodique sur watchlist

config.py : lecture .env (clé API, accès DB)

tests/ : unit tests simples et contrôles de cohérence

🚦 Style
Python 3.10+

Typage fort recommandé

Log explicite (print() ou logging)

Documentation minimale (docstring + README)

approche TTD