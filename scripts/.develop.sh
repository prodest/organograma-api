#!/bin/bash

docker build -t $DOCKER_IMAGE-dev -f ./Dockerfile .

docker login -u="$DOCKER_USERNAME" -p="$DOCKER_PASSWORD"
docker push $DOCKER_IMAGE-dev

export RANCHER_ENV_ID=1a10541 #env processoeletronico (1a10541)
export RANCHER_STACK_ID=1e100 #stack dev (1e100)
export RANCHER_STACK=dev #stack dev (1e100)

/bin/sh .upgrade.sh
