using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Identity.Infrastructure.Context;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var configuration = GetConfigurationRoot();

        var connectionString = configuration.GetConnectionString("IdentityConnection");
        
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql(connectionString,
            b => b.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext))?.GetName().FullName));
        
        return new AppDbContext(optionsBuilder.Options,configuration,null,null);
    }
    
    public static IConfigurationRoot GetConfigurationRoot()
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Identity.Api"))
            .AddJsonFile("appsettings.json", false, true)
            //.AddJsonFile($"appsettings-common.{environment}.json", true)
            .AddEnvironmentVariables("SportsNetworkingPlatform_")
            .Build();

        return configuration;
    }
}