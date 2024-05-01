# SAGA Demo

## Tech Stack
- [x] Rebus (SAGA)
- [x] RabbitMQ
- [x] MediatR
- [x] Docker
- [x] Sql Server

- ![alt text](./doc/saga-demo.gif)

```dotnetcli

$ dotnet new console -n SagaDemo

$ cd SagaDemo

$ dotnet add package Rebus
$ dotnet add package Rebus.RabbitMQ

$ dotnet new webapi -o saga-demo
$ dotnet sln add saga-demo

$ cd saga-demo
$ dotnet add package Rebus
$ dotnet add package Rebus.RabbitMQ
$ dotnet add package Rebus.ServiceProvider
$ dotnet add package Rebus.PostgreSql

$ dotnet build

$ docker-compose up -d

# MQ
http://localhost:15672


```
