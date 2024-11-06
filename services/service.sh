docker stack deploy -c postgres.yml postgres
docker stack deploy -c pgadmin4.yml pgadmin4
docker stack deploy -c elasticsearch.yml elasticsearch
docker stack deploy -c kibana.yml kibana
docker stack deploy -c redis.yml redis
docker stack deploy -c rabbitmq.yml rabbitmq
docker stack deploy -c dbmigrate.yml migrate
docker stack deploy -c pusula_healthcare.yml pusula

