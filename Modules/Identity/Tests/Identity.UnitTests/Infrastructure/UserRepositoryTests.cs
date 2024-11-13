using System.Linq.Expressions;
using AutoMapper;
using FluentAssertions;
using Identity.Core.Domain.Common;
using Identity.Core.Domain.Entities;
using Identity.Infrastructure.Entities;
using Identity.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Identity.UnitTests.Infrastructure;

public class UserRepositoryTests : TestBase
{
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly UserRepository _sut;

    public UserRepositoryTests()
    {
        var userStore = new Mock<IUserStore<ApplicationUser>>();
        _userManagerMock = new Mock<UserManager<ApplicationUser>>(
            userStore.Object, null, null, null, null, null, null, null, null);
        _sut = new UserRepository(_userManagerMock.Object, Mapper);
    }
    
    [Fact]
    public async Task GetByIdAsync_FromDb_WhenUserExists_ReturnsCorrectUser()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Create the in-memory database context
        var context = CreateDbContext();

        // Seed the database with a test user
        var appUser = new ApplicationUser
        {
            Id = userId.ToString(),
            Email = "test@example.com",
            FirstName = "Test",
            LastName = "User",
            Type = Enums.UserType.Driver,
            CreatedAt = DateTime.UtcNow,
            UserName = "test@example.com",
            PhoneNumber = "1234567890",
            Gender = Enums.Gender.Male
        };

        context.Users.Add(appUser);
        await context.SaveChangesAsync();

        // Create a UserStore and UserManager using the in-memory context
        var userStore = new UserStore<ApplicationUser>(context);
        var userManager = new UserManager<ApplicationUser>(
            userStore, null, new PasswordHasher<ApplicationUser>(), null, null, null, null, null, null);

        // Initialize your repository with the real UserManager and Mapper
        var userRepository = new UserRepository(userManager, Mapper);

        // Act
        var result = await userRepository.GetByIdAsync(userId);

        // Assert
        result.Should().NotBeNull();
        result.Email.Should().Be(appUser.Email);
        result.FirstName.Should().Be(appUser.FirstName);
        result.LastName.Should().Be(appUser.LastName);
        result.Should().BeOfType<Driver>();
    }
    [Fact]
    public async Task CreateAsync_WithValidUser_ReturnsTrue()
    {
        // Arrange
        var user = new Driver(
            "test@example.com",
            "Test",
            "User",
            "1234567890",
            DateTime.UtcNow,
            Enums.Gender.Male,
            Enums.UserType.Driver,
            null,
            "Toyota",
            "Red",
            "ABC123"
        );

        _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _sut.CreateAsync(user, "password123");

        // Assert
        result.Succeeded.Should().BeTrue();
        _userManagerMock.Verify(x => x.CreateAsync(
            It.Is<ApplicationUser>(u => 
                u.Email == user.Email && 
                u.Type == Enums.UserType.Driver), 
            "password123"), 
            Times.Once);
    }
}