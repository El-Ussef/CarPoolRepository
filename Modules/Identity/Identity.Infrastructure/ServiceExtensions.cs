using System.Reflection;
using Identity.Core.Application.Contracts;
using Identity.Core.Application.Mappings;
using Identity.Core.Application.Services;
using Identity.Infrastructure.Common;
using Identity.Infrastructure.Context;
using Identity.Infrastructure.Entities;
using Identity.Infrastructure.Repositories;
using Identity.Infrastructure.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure;

public static class ServiceExtensions
{
    public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var originalPath = Directory.GetCurrentDirectory();

        string segmentToRemove = "/CoreApi";

        string modifiedPath;
        
        if (originalPath.EndsWith(segmentToRemove))
        {
            modifiedPath = originalPath.Substring(0, originalPath.Length - segmentToRemove.Length);
        }
        else
        {
            // Handle cases where the segment is not at the end of the path, or you want to preserve the original path
            modifiedPath = originalPath;
        }

        // services.AddDbContext<AppDbContext>(options => 
        //     options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("IdentityConnection"),
                npgsql =>
                {
                    npgsql.EnableRetryOnFailure(3);
                    npgsql.CommandTimeout(30);
                    npgsql.MigrationsAssembly(typeof(AppDbContext).Assembly.GetName().Name);
                });

            // Performance optimizations
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
        });
        services.AddAutoMapper(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(IMapFrom<>)));
        //services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        // services.AddTransient<IFileStorageService, FileStorageService>(provider =>
        //     new FileStorageService(Path.Combine(modifiedPath, "Images")));

        //services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration.GetConnectionString("RabbitMQ"));
                cfg.ConfigureEndpoints(context);
            });
        });
        // Add EventPublisher registration
        //services.AddTransient<IEventPublisher, EventPublisher>();
        
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IAuthenticationService, AuthenticationService>();
        //services.AddTransient<ISeedDataBase, SeedDataService>();
        services.AddTransient<IJwtTokenService,JwtTokenService>();
        services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<AppDbContext>();
    }
}