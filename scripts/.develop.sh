#!/bin/bash

export RANCHER_STACK=prd
export IMAGE_NAME=$DOCKER_IMAGE-dev:$DOCKER_TAG

. ./scripts/.deploy.sh