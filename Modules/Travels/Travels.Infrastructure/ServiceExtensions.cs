using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Travels.Core.Application.Contracts;
using Travels.Core.Application.Services;
using Travels.Infrastructure.Context;

namespace Travels.Infrastructure;

public static class ServiceExtensions
{
    public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITravelService, TravelService>();
        services.AddScoped<ITravelRepository, ITravelRepository>();
        
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                npgsql =>
                {
                    npgsql.EnableRetryOnFailure(3);
                    npgsql.CommandTimeout(30);
                    npgsql.MigrationsAssembly(typeof(AppDbContext).Assembly.GetName().Name);
                });

            // Performance optimizations
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
        });
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

    }
}