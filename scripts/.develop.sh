#!/bin/bash

docker tag $DOCKER_IMAGE $DOCKER_IMAGE-dev

docker login -u="$DOCKER_USERNAME" -p="$DOCKER_PASSWORD"
docker push $DOCKER_IMAGE-dev

export RANCHER_ENV_ID=1a10541 #env SEP/organograma (1a10541)
export RANCHER_STACK_ID=1e100 #stack dev (1e100)
export RANCHER_STACK=dev #stack dev (1e100)

export RANCHER_URL=http://cloud.datacenter.es.gov.br.local/v1/projects/$RANCHER_ENV_ID
export RANCHER_COMPOSE_URL=http://cloud.datacenter.es.gov.br.local/v1/projects/$RANCHER_ENV_ID/environments/$RANCHER_STACK_ID/composeconfig

#Atualiza a infra
git clone https://github.com/prodest/gerencio-upgrade.git
cd gerencio-upgrade
npm install
node ./client $RANCHER_SERVICE_NAME 40000