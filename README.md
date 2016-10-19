AngularMovie ASP.NET Core
=========================

Ce projet est une introduction à [ASP.NET Core](https://www.microsoft.com/net)
Le Front est un fork du projet [AngularMovie](https://github.com/Sfeir/angularmovie-300)
Les données sont stockées dans une base MongoDB.

## Pré-requis
* nodejs
* npm
* docker for mac/windows

## Installation
* Cloner le dépôt (<code>git clone https://github.com/gbrunois/angularmovie-aspnetcore.git</code>)
* Ouvrir le terminal
* Se placer à la racine du projet
* Pour Mac/Linux, exécuter <code>./dockerTask.sh dockerComposeBuildUp</code>
* Pour Windows, exécuter <code>powershell.exe -File dockerTask.ps1 -DockerComposeBuildUp</code>

## Développement
* Pré-requis : Installer [.NET Core SDK](https://www.microsoft.com/net/core)
* Pour Mac/Linux <code>./dockerTask.sh dockerComposeMongoBuildUp</code> pour créer et exécuter l'image docker MongoDB
* Pour Windows <code>powershell.exe -File dockerTask.ps1 -DockerComposeMongoBuildUp</code> pour créer et exécuter l'image docker MongoDB

* Exécuter <code>npm install</code> pour restaurer les paquets NPM
* Exécuter <code>dotnet restore</code> pour restaurer les paquets Nuget
* Exécuter <code>dotnet build</code> pour compiler
* Exécuter <code>dotnet run</code> pour lancer le serveur
* Exécuter <code>dotnet test</code> pour lancer les tests
* Pour tester l'API: <code>curl -I http://localhost:5000/server/api/movies</code>




