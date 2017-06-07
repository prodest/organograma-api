#!/bin/bash
set -e

export RANCHER_ENV=SEP/Organograma
export RANCHER_SERVICE=organograma-api
export RANCHER_START_FIRST=true
export DOCKER_IMAGE=prodest/organograma-api
export DOCKER_TAG=${TRAVIS_COMMIT:0:7}

cd OrganogramaWebAPI/src/WebAPI/
dotnet restore && dotnet publish -c release -r debian.8-x64 -o publish ./

cd ../../../

docker build -t $DOCKER_IMAGE -f ./Dockerfile .