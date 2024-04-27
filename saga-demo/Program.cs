using MediatR;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using saga_demo.Application.Orders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    //    config.AddBehavior<LoggingBehavior>();
});

builder.Services.AddRebus(rebus => rebus
    .Routing(r => r.TypeBased().MapAssemblyOf<Program>("saga-demo-queue"))
    .Transport(t => t.UseRabbitMq(builder.Configuration.GetConnectionString("MessageBroker"), "saga-demo-queue"))
    .Sagas(s => s.StoreInPostgres(builder.Configuration.GetConnectionString("Database"), "postgres", "saga_indexes")),
    onCreated: async bus =>
    {
        await bus.Subscribe<OrderCreatedEvent>();
        await bus.Subscribe<OrderConfirmationEmailSentEvent>();
        await bus.Subscribe<PaymentRequestSentEvent>();
    }
    // .Options(o => o.EnableStateMachineSubscriptions())
);

builder.Services.AutoRegisterHandlersFromAssemblyOf<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapPost("/order", async (IMediator bus) =>
{
    await bus.Send(new OrderCreateCommand(Guid.NewGuid()));

    return Results.Accepted();
});

app.Run();

public partial class Program { }

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
