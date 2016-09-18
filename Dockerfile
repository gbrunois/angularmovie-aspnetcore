FROM microsoft/dotnet:latest

#ENV DOTNET_USE_POLLING_FILE_WATCHER=1

#COPY ./project.json /var/www/aspnetcoreapp/project.json
#COPY ./package.json /var/www/aspnetcoreapp/package.json

#WORKDIR /var/www/aspnetcoreapp

COPY . /app

WORKDIR /app

RUN ["dotnet", "restore"]

RUN ["dotnet", "build"]

EXPOSE 5000/tcp

ENV ASPNETCORE_URLS http://*:5000

#ENTRYPOINT ["dotnet", "watch", "run", "--server.urls", "http://0.0.0.0:5000"]

ENTRYPOINT ["dotnet", "run", "--server.urls", "http://0.0.0.0:5000"]