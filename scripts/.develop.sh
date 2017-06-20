#!/bin/bash

export RANCHER_STACK=dev

# deploy WebAPI
export RANCHER_SERVICE=organograma-api
export IMAGE_NAME=$DOCKER_IMAGE-dev:$DOCKER_TAG
. ./scripts/.deploy.sh

# deploy JOBSCHEDULER
export RANCHER_SERVICE=organograma-jobscheduler
export IMAGE_NAME=$DOCKER_IMAGE_JS-dev:$DOCKER_TAG
export DOCKER_IMAGE=$DOCKER_IMAGE_JS
. ./scripts/.deploy.sh
