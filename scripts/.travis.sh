#!/bin/bash
set -e

export RANCHER_ENV=SEP/Organograma
export RANCHER_START_FIRST=true
export DOCKER_TAG=${TRAVIS_COMMIT:0:7}

# publish WebAPI
export DOCKER_IMAGE=prodest/organograma-api

cd OrganogramaWebAPI/src/WebAPI/
dotnet restore && dotnet publish -c release -r debian.8-x64 -o publish ./

cd ../../../

docker build -t $DOCKER_IMAGE -f ./Dockerfile .


# publish JOBSCHEDULER
export DOCKER_IMAGE_JS=prodest/organograma-jobscheduler

cd OrganogramaWebAPI/src/JobScheduler/
dotnet restore && dotnet publish -c release -r debian.8-x64 -o publish ./

cd ../../../

docker build -t $DOCKER_IMAGE_JS -f ./Dockerfile-JS .