using CarPool.Shared.Events.Interfaces;
using CarPool.Shared.Events.Services;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarPool.Shared.Events;

public static class ServiceExtensions
{
    public static void AddSharedDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDateTimeService, DateTimeService>();
    }
}