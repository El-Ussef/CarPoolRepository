using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarPool.Shared.Events;

public static class ServiceExtensions
{
    public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration.GetConnectionString("RabbitMQ"));
                cfg.ConfigureEndpoints(context);
            });
        });
    }
}