import subprocess
import os

def test_signal_to_db_1h_single_pair():
    # Prépare une wishlist temporaire avec une seule paire
    wishlist_path = "wishlist_symbols.txt"
    backup_path = wishlist_path + ".bak"
    single_pair = "BTC/USDT\n"
    # Sauvegarde l'original
    if os.path.exists(wishlist_path):
        os.rename(wishlist_path, backup_path)
    with open(wishlist_path, "w", encoding="utf-8") as f:
        f.write(single_pair)
    # Exécute le script principal
    try:
        result = subprocess.run(["python", "../main_signal_to_db_1h.py"], capture_output=True, text=True, timeout=180)
        #result = subprocess.run(["python", "main_signal_to_db_1h.py"], capture_output=True, text=True, timeout=180)
        print(result.stdout)
        print(result.stderr)
        assert result.returncode == 0, f"Le script a échoué: {result.stderr}"
    except subprocess.TimeoutExpired:
        print("[TIMEOUT] Le script main_signal_to_db_1h.py a dépassé le délai d'attente (180s). Possible blocage sur appel API ou base de données.")
        assert False, "Timeout du script main_signal_to_db_1h.py"
    finally:
        # Restaure la wishlist d'origine
        if os.path.exists(backup_path):
            os.remove(wishlist_path)
            os.rename(backup_path, wishlist_path)

def main():
    test_signal_to_db_1h_single_pair()

if __name__ == "__main__":
    main()
