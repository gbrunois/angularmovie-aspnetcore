AngularMovie ASP.NET Core

## Installation
* Récupérer les sources (<code>git clone https://github.com/gbrunois/angularmovie-aspnetcore.git</code>)
* Ouvrir le terminal
* Se placer à la racine du projet
* Exécuter <code>npm install</code>
* Exécuter <code>docker-compose build</code> pour construire l'image docker


## Démarrage de l'application
* Exécuter <code>docker-compose up -d</code> pour exécuter l'image docker
* Exécuter <code>docker-compose up -d --build</code> pour metre à jour puis exécuter l'image docker
* Accéder à l'application via l'adresse [http://localhost:5000](http://localhost:5000)

## Développement
* <code>export ASPNETCORE_ENVIRONMENT=development</code>
* Exécuter <code>docker-compose -f docker-compose.mongo.yml up --build -d</code> pour exécuter l'image docker MongoDB
* Exécuter <code>dotnet restore</code> pour restaurer les paquets Nuget
* Exécuter <code>dotnet build</code> pour compiler
* Exécuter <code>dotnet run</code> pour lancer le serveur
* Exécuter <code>dotnet test</code> pour lancer les tests

* Pour tester l'API: <code>curl -I http://localhost:5000/server/api/movies</code>




