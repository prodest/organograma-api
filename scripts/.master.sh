#!/bin/bash

export RANCHER_STACK=prd

# deploy WebAPI
export RANCHER_SERVICE=organograma-api
export IMAGE_NAME=$DOCKER_IMAGE-prd:$DOCKER_TAG
. ./scripts/.deploy.sh

# deploy JOBSCHEDULER
export RANCHER_SERVICE=organograma-jobscheduler
export IMAGE_NAME=$DOCKER_IMAGE_JS-prd:$DOCKER_TAG
export DOCKER_IMAGE=$DOCKER_IMAGE_JS
. ./scripts/.deploy.sh
