"""
END.md - Guide de bonnes pratiques pour la documentation, la qualité du code Python et la démarche de développement

Ce fichier résume les conventions à appliquer systématiquement dans tous les projets Python pour garantir la lisibilité, la maintenabilité et la qualité du code.

## 1. Conventions de nommage (PEP 8)
- Fonctions et variables : snake_case (ex : ma_fonction, valeur_max)
- Classes : PascalCase (ex : MaClasse)
- Constantes : UPPER_CASE (ex : NOMBRE_MAX)
- Fichiers : snake_case (ex : utils.py, test_utils.py)

## 2. Docstrings (PEP 257)
- Chaque module, classe, fonction et méthode doit avoir une docstring explicative.
- Utiliser le format triple guillemets """ pour les docstrings.
- Décrire le rôle, les arguments, les valeurs de retour et les exceptions éventuelles.

Exemple pour une fonction :

    def addition(a: int, b: int) -> int:
        """
        Additionne deux entiers.

        Args:
            a (int): Premier entier.
            b (int): Deuxième entier.

        Returns:
            int: La somme des deux entiers.
        """
        return a + b

## 3. Structure des modules
- Un en-tête de module avec une docstring décrivant le fichier.
- Importations groupées en haut de fichier.
- Fonctions utilitaires regroupées dans utils.py.
- Tests unitaires dans un dossier tests/ avec des fichiers test_*.py.

## 4. Test Driven Development (TDD)
- Tous les projets suivent la démarche TDD : on écrit d'abord les tests unitaires avant d'implémenter les fonctionnalités.
- Les tests sont systématiquement présents, maintenus et exécutés à chaque évolution du code.
- La couverture de tests doit être maximale pour garantir la robustesse et la maintenabilité.

## 5. Autres bonnes pratiques
- Un fichier requirements.txt pour les dépendances.
- Un README.md pour décrire le projet, l'installation et l'utilisation.
- Respecter la structure du projet (src/, tests/, etc.).
- Utiliser un linter (ex : flake8) pour vérifier le respect de PEP 8.
- Ajouter des commentaires pertinents si nécessaire, mais privilégier la clarté du code et des docstrings.

---

Ces conventions et cette démarche TDD sont à appliquer systématiquement pour tous les nouveaux projets Python.
"""
