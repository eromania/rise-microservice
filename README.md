# rise-microservice

![](rise-microservice.drawio.png)

# run

- rabbitmq and postgresql on docker

docker-compose up -d

TODO: setup services, workers and frontend for docker-compose 

# Service Urls

- RabbitMQ: http://localhost:15672
- postgresql: localhost:5432
  - user: postgres, password: rise
- api gateway: http://localhost:5001
- contact service: http://localhost:5005
  - worker: console app Pub/Sub
- report service: http://localhost:5007
  - worker: console app Pub/Sub
- vue3 app: http://localhost:8080