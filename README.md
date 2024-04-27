# SAGA Demo

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

```
