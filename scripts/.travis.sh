#!/bin/bash

cd OrganogramaWebAPI/src/WebAPI/
dotnet restore && dotnet publish -c release -r debian.8-x64 -o ./publish

cd ../../../

docker build -t $DOCKER_IMAGE -f ./Dockerfile .
