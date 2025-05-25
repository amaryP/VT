# Batch PowerShell pour lancer les scans sur 1h, 15m et 5m pour la wishlist

Write-Host "[INFO] Lancement du scan 1h..."
python main_signal_to_db_1h.py

Write-Host "[INFO] Lancement du scan 15m..."
python main_signal_to_db_15m.py

Write-Host "[INFO] Lancement du scan 5m..."
python main_signal_to_db_5m.py

Write-Host "[INFO] Tous les scans sont terminés."
