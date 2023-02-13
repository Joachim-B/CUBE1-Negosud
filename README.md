# CUBE1 Negosud

Les livrables attendus pour le CUBE 1, avec un guide d'installation fourni.

## Guide d'installation

### Prérequis
- Environnement MySQL (MySQL Server, MySQL Workbench) - [Installer MySQL Community](https://dev.mysql.com/downloads/installer/)
- Visual Studio (pour lancer l'API en Debug) - [Installer Visual Studio 2022](https://visualstudio.microsoft.com/fr/downloads/)
- XAMPP (pour lancer le site web) - [Installer XAMPP](https://www.apachefriends.org/download.html)

### Récupérer la base de données

- Ouvrir MySQL Workbench, et la connexion souhaitée
- Ouvrir et exécuter le fichier `negosud_database.sql` situé dans le dossier `Database` afin de créer la base de données

### Configurer l'API

- Ouvrir le fichier `dbsettings.json`, situé dans le dossier `API`
- Renseigner les identifiants nécessaires pour se connecter à la base de données, et enregistrer 
- Ouvrir la solution de l'API dans Visual Studio et l'exécuter en Debug

### Configurer le client lourd

- Ouvrir le fichier `connection_url.txt` situé dans le dossier `Heavy client`
- Renseigner l'url de l'API et enregistrer
- Exécuter l'application du client lourd située à l'adresse `Heavy client\Gestion stock\bin\Debug\net6.0-windows\Gestion stock.exe`
- Pour se connecter au client lourd, le nom d'utilisateur et le mot de passe ne doivent pas être renseignés

### Configurer le site web

- Ouvrir le fichier `api_url.txt` situé à l'adresse `Website\NegoSud\data\api_url.txt`
- Renseigner l'url de l'API et enregistrer
- Ouvrir le dossier `xampp` de l'ordinateur contenant tous les fichiers de XAMPP
- Copier le dossier `NegoSud` situé dans le dossier `Website` à l'intérieur du dossier `htdocs` situé dans le dossier `xampp`
- Ouvrir l'application XAMPP Control Panel, et démarrer Apache
- Dans le navigateur web, ouvrir l'url `<adresse de XAMPP>/NegoSud`
