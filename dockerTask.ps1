<#
.SYNOPSIS
Builds and Run a Docker image.
.PARAMETER DockerComposeBuild
Builds a Docker image.
.PARAMETER DockerComposeUp
Run docker-compose.
.PARAMETER DockerComposeBuildUp
Build ans run docker-compose.
.PARAMETER DockerComposeMongoBuild
Builds a Docker image MongoDB.
.PARAMETER DockerComposeMongoUp
Run docker-compose MongoDB.
.PARAMETER DockerComposeMongoBuildUp
Build and run docker-compose MongoDB.
.PARAMETER Clean
Removes the image angularmovie-aspnetcore and kills all containers based on that image.

.EXAMPLE
C:\PS> .\dockerTask.ps1 -DockerComposeBuild
Build a Docker image
#>

Param(
    [Parameter(Mandatory=$True,ParameterSetName="DockerComposeBuild")]
    [switch]$DockerComposeBuild,
    [Parameter(Mandatory=$True,ParameterSetName="DockerComposeUp")]
    [switch]$DockerComposeUp,
    [Parameter(Mandatory=$True,ParameterSetName="DockerComposeBuildUp")]
    [switch]$DockerComposeBuildUp,
    [Parameter(Mandatory=$True,ParameterSetName="DockerComposeMongoBuild")]
    [switch]$DockerComposeMongoBuild,
    [Parameter(Mandatory=$True,ParameterSetName="DockerComposeMongoUp")]
    [switch]$DockerComposeMongoUp,
    [Parameter(Mandatory=$True,ParameterSetName="DockerComposeMongoBuildUp")]
    [switch]$DockerComposeMongoBuildUp,
    [Parameter(Mandatory=$True,ParameterSetName="Clean")]
    [switch]$Clean
)

$projectName="angularmovie-aspnetcore"
$projetDir="src\MoviesApi"

# Kills all running containers of an image and then removes them.
function CleanAll () {
    $composeFileName = "$projetDir\docker-compose.yml"
    
    docker-compose -f "$composeFileName" -p $projectName down --rmi all

    $danglingImages = $(docker images -q --filter 'dangling=true')
    if (-not [String]::IsNullOrWhiteSpace($danglingImages)) {
        docker rmi -f $danglingImages
    }
}

# Builds the Docker image.
function DockerComposeBuild () {
    $composeFileName = "$projetDir\docker-compose.yml"

    Write-Host "Building the project."
    Push-Location $projetDir
    npm install
    Pop-Location

    Write-Host "Building the image $imageName."
    docker-compose -f $composeFileName -p $projectName build
}

# Builds the Docker image MongoDB.
function DockerComposeMongoBuild () {
    $composeFileName="$projetDir\docker-compose.mongo.yml" 
  
    Write-Host "Building the image MongoDB."
    docker-compose -f $composeFileName -p $projectName build
}

# Run docker-compose.
function DockerComposeUp () {
    $composeFileName = "$projetDir\docker-compose.yml"

    Write-Host "Running compose file $composeFileName"
    docker-compose -f $composeFileName -p $projectName kill
    docker-compose -f $composeFileName -p $projectName up -d
}

# Run docker-compose MongoDB.
function DockerComposeMongoUp () {
    $composeFileName = "$projetDir\docker-compose.mongo.yml"

    Write-Host "Running compose file $composeFileName"
    docker-compose -f $composeFileName -p $projectName kill
    docker-compose -f $composeFileName -p $projectName up -d
}

# Opens the remote site
function OpenSite ([string]$url) {
    Write-Host "Opening site" -NoNewline
    $status = 0

    #Check if the site is available
    while($status -ne 200) {
        try {
            $response = Invoke-WebRequest -Uri $url -Headers @{"Cache-Control"="no-cache";"Pragma"="no-cache"} -UseBasicParsing
            $status = [int]$response.StatusCode
        }
        catch [System.Net.WebException] { }
        if($status -ne 200) {
            Write-Host "." -NoNewline
            Start-Sleep 1
        }
    }

    Write-Host
    # Open the site.
    Start-Process $url
}

# Call the correct function for the parameter that was used
if($DockerComposeBuild) {
    DockerComposeBuild
}
elseif($DockerComposeUp) {
    DockerComposeUp
}
elseif($DockerComposeBuildUp) {
    DockerComposeBuild
    DockerComposeUp
    OpenSite http://localhost:5000
}
elseif($DockerComposeMongoBuild) {
    DockerComposeMongoBuild
}
elseif($DockerComposeMongoUp) {
    DockerComposMongoUp
}
elseif($DockerComposeMongoBuildUp) {
    DockerComposeMongoBuild
    DockerComposeMongoUp
}
elseif ($Clean) {
    CleanAll
}