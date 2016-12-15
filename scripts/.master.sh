#!/bin/bash

docker build -t $DOCKER_IMAGE -f ./Dockerfile .

docker login -u="$DOCKER_USERNAME" -p="$DOCKER_PASSWORD"
docker push $DOCKER_IMAGE

export RANCHER_ENV_ID=1a10541 #env processoeletronico (1a10541)
export RANCHER_STACK_ID=1e231 #stack prd (1e231)
export RANCHER_STACK=prd #stack prd (1e231)

/bin/sh .upgrade.sh
