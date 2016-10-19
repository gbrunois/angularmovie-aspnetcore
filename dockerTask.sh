#!/bin/bash

projectName="angularmovie-aspnetcore"
DOCKER_IP="localhost"

if [ -n "$DOCKER_MACHINE_NAME" ]; then 
    DOCKER_IP=$(docker-machine ip $DOCKER_MACHINE_NAME); 
fi

# Kills all running containers of an image and then removes them.
cleanAll () {
    echo "Clean all"
    composeFileName="docker-compose.yml"
    docker-compose -f $composeFileName -p $projectName down --rmi all

    # Remove any dangling images (from previous builds)
    danglingImages=$(docker images -q --filter 'dangling=true')
    if [[ ! -z $danglingImages ]]; then
      docker rmi -f $danglingImages
    fi
}

# Builds the Docker image.
dockerComposeBuild () {
  
    composeFileName="docker-compose.yml"
  
    echo "Building the project."
    npm install
    echo "Building the image $projectName."
    docker-compose -f $composeFileName -p $projectName build
}

# Builds the Docker image MongoDB.
dockerComposeMongoBuild () {
  
    composeFileName="docker-compose.mongo.yml"
  
    echo "Building the image MongoDB."
    docker-compose -f $composeFileName -p $projectName build
}

# Run docker-compose.
dockerComposeUp () {
  
    composeFileName="docker-compose.yml"
  
    echo "Running compose file $composeFileName"
    docker-compose -f $composeFileName -p $projectName kill
    docker-compose -f $composeFileName -p $projectName up -d
}

# Run docker-compose mongoDB only.
dockerComposeMongoUp () {
  
    composeFileName="docker-compose.mongo.yml"
  
    echo "Running compose file $composeFileName"
    docker-compose -f $composeFileName -p $projectName kill
    docker-compose -f $composeFileName -p $projectName up -d
}

openSite () {
  url=$1
  printf 'Opening site'
  until $(curl --output /dev/null --silent --head --fail $url); do
    printf '.'
    sleep 1
  done

  # Open the site.
  open $url
}

# Shows the usage for the script.
showUsage () {
  echo "Usage: dockerTask.sh [COMMAND]"
  echo ""
  echo "Commands:"
  echo "    dockerComposeBuild: Builds a Docker image (Web server and MongoDB)."
  echo "    dockerComposeBuildMongo: Builds a Docker image (MongoDB)."
  echo "    dockerComposeUp: Run docker-compose."
  echo "    dockerComposeMongoUp: Run docker-compose.mongo"
  echo "    dockerComposeBuildUp: Build and run docker-compose"
  echo "    dockerComposeMongoBuildUp: Build and run docker-compose (MongoDB)"
  echo "    clean: Removes the image '$projectName' and kills all containers based on that image."
  echo "Example:"
  echo "    ./dockerTask.sh dockerComposeBuildUp"
  echo ""
}

if [ $# -eq 0 ]; then
  showUsage
else
  case "$1" in
    "dockerComposeBuild")
            dockerComposeBuild
            ;;
    "dockerComposeMongoBuild")
            dockerComposeMongoBuild
            ;;
    "dockerComposeUp")
            dockerComposeUp
            ;;
    "dockerComposeMongoUp")
            dockerComposeMongoUp
            ;;
    "dockerComposeBuildUp")
            dockerComposeBuild
            dockerComposeUp
            openSite http://$DOCKER_IP:5000
            ;;
    "dockerComposeMongoBuildUp")
            dockerComposeMongoBuild
            dockerComposeMongoUp
            ;;
    "clean")
            cleanAll
            ;;
    *)
            showUsage
            ;;
  esac
fi