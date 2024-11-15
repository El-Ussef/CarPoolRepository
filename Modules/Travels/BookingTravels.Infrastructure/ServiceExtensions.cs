using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BookingTravels.Core.Application.Contracts;
using BookingTravels.Core.Application.Contracts.Travels;
using BookingTravels.Core.Application.Services;
using BookingTravels.Infrastructure.Context;

namespace BookingTravels.Infrastructure;

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