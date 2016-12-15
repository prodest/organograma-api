#!/bin/bash

docker build -t $DOCKER_IMAGE-hmg -f ./Dockerfile .

docker login -u="$DOCKER_USERNAME" -p="$DOCKER_PASSWORD"
docker push $DOCKER_IMAGE-hmg

export RANCHER_ENV_ID=1a10541 #env processoeletronico (1a10541)
export RANCHER_STACK_ID=1e102 #stack hmg (1e102)
export RANCHER_STACK=hmg #stack hmg (1e102)

/bin/sh .upgrade.sh
