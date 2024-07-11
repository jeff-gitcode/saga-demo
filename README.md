# SAGA Demo

## Tech Stack
- [x] Rebus (SAGA)
- [x] RabbitMQ
- [x] MediatR
- [x] Docker
- [x] Sql Server

- ![alt text](./doc/saga-demo.gif)

```dotnetcli

$dotnet new sln --name saga

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
$ docker-compose down
# MQ
http://localhost:15672


# saga demo2
$ dotnet new webapi -o saga-demo2

$ dotnet build
$ dotnet run --project ./saga-demo2

$ dotnet add ./saga-demo2 package MassTransit
$ dotnet add ./saga-demo2 package MediatR
$ dotnet add ./saga-demo2 package MassTransit.Extensions.DependencyInjection
$ dotnet add ./saga-demo2 package MassTransit.RabbitMQ
$ dotnet add ./saga-demo2 package EntityFramework
$ dotnet add ./saga-demo2 package MassTransit.EntityFrameworkCore
$ dotnet add ./saga-demo2 package Microsoft.EntityFrameworkCore.Tools
$ dotnet add ./saga-demo2 package Npgsql.EntityFrameworkCore.PostgreSQL
$ dotnet add ./saga-demo2 package Microsoft.EntityFrameworkCore.Design
$ dotnet sln add saga-demo2

# TO CREATE TABLE IN POSTGRES
$ dotnet ef migrations add InitialMigration
$ dotnet ef database update
```
