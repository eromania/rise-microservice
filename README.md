# rise-microservice

![](rise-microservice.drawio.png)

# run

docker-compose up -d

# Service Urls

- RabbitMQ: http://localhost:15672
- postgresql: localhost:5432
  - user: postgres, password: rise
- consul: http://localhost:8500
- api gateway: http://localhost:5000
- contact service: http://localhost:5001
- user service: http://localhost:5002
- report service: http://localhost:5003
- vue3 app: http://localhost:8080