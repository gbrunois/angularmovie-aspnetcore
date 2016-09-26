AngularMovie ASP.NET Core
=========================

[![Build Status](https://travis-ci.org/gbrunois/angularmovie-aspnetcore.svg?branch=master)](https://travis-ci.org/gbrunois/angularmovie-aspnetcore)

Librairie de films développée avec AngularJS (fork [https://github.com/Sfeir/angularmovie-300](https://github.com/Sfeir/angularmovie-300)) et servie avec ASP.NET Core. Les données sont stockées dans une base MongoDB


## Pré-requis
* Installer docker
* NPM (brew install npm , for OS X users. Windows users can Download NPM as part of Node.JS)
* Bower (npm install -g bower)


## Installation
* Récupérer les sources (<code>git clone git@github.com:gbrunois/angularmovie-aspnetcore.git</code>)
* Ouvrir le terminal
* Se placer à la racine du projet
* Exécuter <code>npm install</code>
* Exécuter <code>docker-compose build</code> pour construire l'image docker


## Démarrage de l'application
* Exécuter <code>docker-compose up -d</code> pour exécuter l'image docker
* Exécuter <code>docker-compose up -d --build</code> pour metre à jour puis exécuter l'image docker
* Accéder à l'application via l'adresse [http://192.168.99.100:5000](http://192.168.99.100:5000)

## Développement
* Exécuter <code>docker-compose -f docker-compose.mongo.yml up --build -d</code> pour exécuter l'image docker MongoDB
* Exécuter <code>dotnet restore</code> pour restaurer les paquets Nuget
* Exécuter <code>dotnet build</code> pour compiler
* Exécuter <code>dotnet run</code> pour lancer le serveur

## Troubleshootings



