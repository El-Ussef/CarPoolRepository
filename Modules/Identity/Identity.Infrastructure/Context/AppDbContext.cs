using Identity.Core.Application.Contracts;
using Identity.Core.Domain.Common;
using Identity.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    private readonly IConfiguration _configuration;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTime;
    
    // Use constructor injection
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Performance: Create indexes for frequently queried fields
        modelBuilder.Entity<ApplicationUser>(builder =>
        {
            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.Type);  // For filtering by user type
            
            builder.Property(e => e.Type)
                .HasConversion<string>()
                .HasColumnName("UserType")
                .IsRequired();  // Ensure not null for better query performance

            // Optimize string fields with max length
            builder.Property(e => e.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(e => e.LastName).HasMaxLength(50).IsRequired();
            builder.Property(e => e.Email).HasMaxLength(100).IsRequired();
            builder.Property(e => e.CreatedAt).IsRequired();
            builder.Property(e => e.ModifiedAt);

            // Optional fields
            builder.Property(e => e.CarBrand).HasMaxLength(50).IsRequired(false);
            builder.Property(e => e.CarColor).HasMaxLength(30).IsRequired(false);
            builder.Property(e => e.CarNumber).HasMaxLength(20).IsRequired(false);
        });

        // Identity configurations
        ConfigureIdentityTables(modelBuilder);
    }

    // Extract identity configuration for cleaner code
    private void ConfigureIdentityTables(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUserLogin<string>>(b => 
            b.HasKey(l => new { l.LoginProvider, l.ProviderKey }));

        modelBuilder.Entity<IdentityUserRole<string>>(b => 
            b.HasKey(r => new { r.UserId, r.RoleId }));

        modelBuilder.Entity<IdentityUserToken<string>>(b => 
            b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name }));
    }

    // Performance: Override SaveChanges to batch updates
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<ITimeModification>().ToList();
        var timestamp = _dateTime.Now.ToUniversalTime();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = timestamp;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedAt = timestamp;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}