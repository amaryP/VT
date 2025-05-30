import os
import sys
import winreg

KEY_NAME = "TAAPI_KEY"


def get_env_key():
    # Check if key is already set in current environment
    return os.environ.get(KEY_NAME)


def set_user_env_var(key, value):
    # Set environment variable persistently for current user (Windows)
    try:
        with winreg.OpenKey(winreg.HKEY_CURRENT_USER,
                            r'Environment', 0, winreg.KEY_SET_VALUE) as regkey:
            winreg.SetValueEx(regkey, key, 0, winreg.REG_SZ, value)
        return True
    except Exception as e:
        print(f"Erreur lors de l'écriture dans la base de registre: {e}")
        return False


def main():
    current = get_env_key()
    if current:
        print(f"Clé TAAPI déjà présente dans l'environnement: {current}")
        answer = input("Voulez-vous la remplacer ? (o/N): ").strip().lower()
        if answer != 'o':
            print("Aucune modification effectuée.")
            return
    # Demande la clé à l'utilisateur
    new_key = input("Entrez votre clé TAAPI.IO: ").strip()
    if not new_key:
        print("Clé vide, opération annulée.")
        return
    if set_user_env_var(KEY_NAME, new_key):
        print(f"Clé TAAPI enregistrée pour l'utilisateur. Elle sera disponible dans les nouveaux terminaux après redémarrage du shell ou déconnexion/reconnexion.")
    else:
        print("Erreur lors de l'enregistrement de la clé.")

if __name__ == "__main__":
    if os.name != 'nt':
        print("Ce script ne fonctionne que sous Windows.")
        sys.exit(1)
    main()
