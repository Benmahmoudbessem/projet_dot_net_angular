# Guide de déploiement et de test du système de gestion de bibliothèque

Ce document fournit les instructions pour tester et déployer l'application de gestion de bibliothèque.

## Prérequis
- .NET SDK 7.0 ou supérieur
- Node.js et npm
- Angular CLI

## Configuration de la base de données

1. Ouvrez le fichier `BibliothequeAPI/appsettings.json` et configurez la chaîne de connexion à votre base de données SQL Server :

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=votre_serveur;Database=BibliothequeDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

2. Exécutez les migrations pour créer la base de données :

```bash
cd BibliothequeAPI
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Test de l'application

### Backend

1. Lancez le backend .NET :

```bash
cd BibliothequeAPI
dotnet run
```

2. Testez l'API avec Swagger en accédant à `https://localhost:7001/swagger`

### Frontend

1. Lancez le frontend Angular :

```bash
cd BibliothequeClient
ng serve
```

2. Accédez à l'application dans votre navigateur : `http://localhost:4200`

3. Testez les fonctionnalités suivantes :
   - Ajout, modification et suppression de livres
   - Ajout, modification et suppression d'utilisateurs
   - Enregistrement d'emprunts et retours de livres

## Déploiement en production

### Backend .NET

1. Publiez l'API :

```bash
cd BibliothequeAPI
dotnet publish -c Release -o ./publish
```

2. Déployez le contenu du dossier `publish` sur votre serveur web (IIS, Azure, etc.)

### Frontend Angular

1. Créez une version de production :

```bash
cd BibliothequeClient
ng build --prod
```

2. Déployez le contenu du dossier `dist/BibliothequeClient` sur votre serveur web

3. Assurez-vous de configurer les URL de l'API dans les services Angular pour qu'elles pointent vers votre API déployée.

## Sécurité et considérations supplémentaires

- Ajoutez une authentification et une autorisation pour sécuriser l'API
- Implémentez HTTPS pour toutes les communications
- Configurez des sauvegardes régulières de la base de données
- Mettez en place une journalisation des erreurs et des activités
