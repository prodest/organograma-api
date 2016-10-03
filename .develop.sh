#!/bin/bash
 
docker build -t $DOCKER_IMAGE-dev .

docker login -u="$DOCKER_USERNAME" -p="$DOCKER_PASSWORD"
docker push $DOCKER_IMAGE-dev

export RANCHER_URL=http://cloud.datacenter.es.gov.br.local/v1/projects/1a10541 #env processoeletronico (1a10541)
export RANCHER_COMPOSE_URL=http://cloud.datacenter.es.gov.br.local/v1/projects/1a10541/environments/1e100/composeconfig #stack dev (1e100)
export RANCHER_STACK=dev

git clone https://github.com/prodest/gerencio-upgrade.git
cd gerencio-upgrade
npm install
node ./client organograma-api 40000
