using System.Collections.Immutable;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using saga_demo2.cqrs;
using saga_demo2.saga;

namespace saga_demo2.ServiceCollectionExtensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMediatorServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(Program).Assembly);
            //    config.AddBehavior<LoggingBehavior>();
        });

        return services;
    }

    public static IServiceCollection AddDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Postgres")!);
        });
        return services;
    }

    public static IServiceCollection AddBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(o =>
        {
            o.SetKebabCaseEndpointNameFormatter();

            o.AddConsumers(typeof(Program).Assembly);

            o.AddSagaStateMachine<NewsLetterOnboardingSaga, NewsLetterOnboardingSagaData>()
                .EntityFrameworkRepository(r=>{
                    r.ExistingDbContext<AppDbContext>();

                    r.UsePostgres();
                });
                
            o.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri(configuration.GetConnectionString("RabbitMq")!), hst=> {
                    hst.Username("guest");
                    hst.Password("guest");
                });

                cfg.UseInMemoryOutbox(context);

                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
