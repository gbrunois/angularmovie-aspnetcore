AngularMovie ASP.NET Core
=========================

* Exécuter <code>dotnet restore</code> pour restaurer les paquets Nuget
* Exécuter <code>dotnet build</code> pour compiler
* Exécuter <code>dotnet test</code> pour lancer les tests
* Exécuter <code>dotnet run</code> pour lancer le serveur

Ajouter un fichier de configuration : appsettings.json avec une section "Mongo"<br />
La section "Mongo" contient les paramètres suivants:
* "ConnectionString": "mongo:27017",
* "DatabaseName": "angularmovies",
* "Username": "angularUser",
* "Password": "password"

Ajouter un fichier de configuration : appsettings.development.json.<br />
Le paramètre "ConnectionString" de la section Mongo devient: "192.168.99.100:27017"

Configurer Startup.cs pour obtenir un typage fort de la section Mongo : (Web.MoviesApi.Repositories.MongoDB.MongoSettings)

Ajouter une section "Logging" avec les paramètres suivants:<br />
* "IncludeScopes": false,
* "LogLevel": {
*           "Default": "Debug",
*           "System": "Information",
*           "Microsoft": "Information"
*       }