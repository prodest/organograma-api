#!/bin/bash

export RANCHER_URL=http://cloud.datacenter.es.gov.br.local/v1/projects/$RANCHER_ENV_ID
export RANCHER_COMPOSE_URL=http://cloud.datacenter.es.gov.br.local/v1/projects/$RANCHER_ENV_ID/environments/$RANCHER_STACK_ID/composeconfig
export RANCHER_SERVICE_NAME=processoeletronico-api

#Atualiza a infra
git clone https://github.com/prodest/gerencio-upgrade.git
cd gerencio-upgrade
npm install
node ./client $RANCHER_SERVICE_NAME 40000
