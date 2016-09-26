#!/bin/bash
 
docker build -t $DOCKER_IMAGE-dev -f ./Dockerfile .

docker login -u="$DOCKER_USERNAME" -p="$DOCKER_PASSWORD"
docker push $DOCKER_IMAGE-dev

export RANCHER_URL=http://cloud.datacenter.es.gov.br.local/v1/projects/9x99999 #env organograma (9x99999)
export RANCHER_COMPOSE_URL=http://cloud.datacenter.es.gov.br.local/v1/projects/9x99999/environments/9x999/composeconfig #stack dev (9x999)

git clone https://github.com/prodest/gerencio-upgrade.git
cd gerencio-upgrade
npm install
node ./client organograma-api 40000
