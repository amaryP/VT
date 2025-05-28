# Script PowerShell pour enregistrer la clé TAAPI.IO dans les variables d'environnement utilisateur
# Usage : clic droit > Exécuter avec PowerShell OU lancer dans un terminal PowerShell

$envName = "KEY_TAAPI_IO"
$envValue = Read-Host -Prompt "Entrez votre clé TAAPI.IO (elle ne sera pas affichée)"

[System.Environment]::SetEnvironmentVariable($envName, $envValue, "User")

Write-Host "La variable d'environnement $envName a été enregistrée pour l'utilisateur courant."
Write-Host "Vous devrez redémarrer votre terminal ou votre session pour qu'elle soit prise en compte dans Python."


