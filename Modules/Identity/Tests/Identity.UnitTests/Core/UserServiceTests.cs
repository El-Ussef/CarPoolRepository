using FluentAssertions;
using Identity.Core.Application.Commons;
using Identity.Core.Application.Contracts;
using Identity.Core.Application.DTOs;
using Identity.Core.Application.Services;
using Identity.Core.Domain.Common;
using Identity.Core.Domain.Entities;
using Moq;

namespace Identity.UnitTests.Core;

public class UserServiceTests : TestBase
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UserService _sut;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _sut = new UserService(_userRepositoryMock.Object, Mapper,null);
    }

    [Fact]
    public async Task RegisterDriverAsync_WithValidData_ReturnsTrue()
    {
        // Arrange
        var request = new RegisterDriverRequest(
            Email: "driver@example.com",
            Password: "Password123!",
            FirstName: "John",
            LastName: "Doe",
            PhoneNumber: "1234567890",
            DateOfBirth: DateTime.UtcNow.AddYears(-25),
            Gender: Enums.Gender.Male,
            ProfilePictureUrl: null,
            CarBrand: "Toyota",
            CarColor: "Red",
            CarNumber: "ABC123"
        );

        _userRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Driver>(), request.Password))
            .ReturnsAsync(Result.Success);

        // Act
        var result = await _sut.RegisterDriverAsync(request);

        // Assert
        result.Succeeded.Should().BeTrue();
        _userRepositoryMock.Verify(x => x.CreateAsync(
            It.Is<Driver>(d => 
                d.Email == request.Email && 
                d.CarBrand == request.CarBrand), 
            request.Password), 
            Times.Once);
    }

    [Fact]
    public async Task RegisterClientAsync_WithValidData_ReturnsTrue()
    {
        // Arrange
        var request = new RegisterClientRequest(
            Email: "client@example.com",
            Password: "Password123!",
            FirstName: "Jane",
            LastName: "Doe",
            PhoneNumber: "1234567890",
            DateOfBirth: DateTime.UtcNow.AddYears(-25),
            Gender: Enums.Gender.Female,
            ProfilePictureUrl: null
        );

        _userRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Client>(), request.Password))
            .ReturnsAsync(Result.Success);

        // Act
        var result = await _sut.RegisterClientAsync(request);

        // Assert
        result.Succeeded.Should().BeTrue();
        _userRepositoryMock.Verify(x => x.CreateAsync(
            It.Is<Client>(c => 
                c.Email == request.Email && 
                c.FirstName == request.FirstName), 
            request.Password), 
            Times.Once);
    }
}