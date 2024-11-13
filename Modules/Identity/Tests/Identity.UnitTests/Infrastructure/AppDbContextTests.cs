using FluentAssertions;
using Identity.Core.Domain.Common;
using Identity.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.UnitTests.Infrastructure;

public class AppDbContextTests : TestBase
{
    [Fact]
    public async Task SaveChangesAsync_SetsAuditProperties()
    {
        // Arrange
        var now = DateTime.UtcNow;
        DateTimeServiceMock.Setup(x => x.Now).Returns(now);
        
        using var context = CreateDbContext();
        var user = new ApplicationUser
        {
            Email = "test@example.com",
            FirstName = "Test",
            LastName = "User",
            Type = Enums.UserType.Client
        };

        // Act
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        // Assert
        var savedUser = await context.Users.FirstAsync();
        savedUser.CreatedAt.Should().Be(now);
    }

    [Fact]
    public void OnModelCreating_ConfiguresIndexesCorrectly()
    {
        // Arrange
        using var context = CreateDbContext();
        var model = context.Model;
        var entityType = model.FindEntityType(typeof(ApplicationUser));

        // Assert
        entityType.Should().NotBeNull();
        entityType.GetIndexes().Should().Contain(i => 
            i.Properties.Any(p => p.Name == "Email") && i.IsUnique);
        entityType.GetIndexes().Should().Contain(i => 
            i.Properties.Any(p => p.Name == "Type"));
    }
}