# Script PowerShell pour exécuter les tests d'intégration sur tous les intervalles et journaliser les résultats

Write-Host "--- Lancement du test d'intégration 1h ---"
python main_signal_to_db_1h.py

Write-Host "--- Lancement du test d'intégration 15m ---"
python main_signal_to_db_15m.py

Write-Host "--- Fin des tests d'intégration ---"
