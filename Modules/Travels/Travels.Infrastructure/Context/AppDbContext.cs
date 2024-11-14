using CarPool.Shared.Events.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Travels.Core.Domain.Entities;

namespace Travels.Infrastructure.Context;

public class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTime;
    
    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        IConfiguration configuration,
        ICurrentUserService currentUserService,
        IDateTimeService dateTime) 
        : base(options)
    {
        _configuration = configuration;
        _currentUserService = currentUserService;
        _dateTime = dateTime;
    }


    public DbSet<Travel> Travels { get; set; }

    public DbSet<Driver> Drivers { get; set; }
    
    public DbSet<Car> Cars { get; set; }

}