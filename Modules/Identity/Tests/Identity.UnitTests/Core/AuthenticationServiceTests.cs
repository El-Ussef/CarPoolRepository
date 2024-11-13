using AutoMapper;
using FluentAssertions;
using Identity.Core.Application.Contracts;
using Identity.Core.Application.DTOs;
using Identity.Core.Application.Services;
using Identity.Core.Domain.Common;
using Identity.Core.Domain.Entities;
using Moq;

namespace Identity.UnitTests.Core;

public class AuthenticationServiceTests : TestBase
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IJwtTokenService> _jwtTokenServiceMock;
    private readonly AuthenticationService _sut;

    public AuthenticationServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _jwtTokenServiceMock = new Mock<IJwtTokenService>();
        _sut = new AuthenticationService(_userRepositoryMock.Object, _jwtTokenServiceMock.Object, Mapper);
    }

    [Fact]
    public async Task AuthenticateAsync_WithValidCredentials_ReturnsAuthenticationResult()
    {
        // Arrange
        var request = new LoginRequest(
            Email: "test@example.com",
            Password: "Password123!"
        );
        var userId = Guid.NewGuid();
        var user = new Driver(
            request.Email, 
            "Test", 
            "User", 
            "1234567890", 
            DateTime.UtcNow.AddYears(-25),
            Enums.Gender.Male, 
            Enums.UserType.Driver, 
            null, 
            "Toyota", 
            "Red", 
            "ABC123"
        );
        var token = "jwt_token";

        _userRepositoryMock.Setup(x => x.AuthenticateAsync(request.Email, request.Password))
            .ReturnsAsync(userId);
        _userRepositoryMock.Setup(x => x.GetByIdAsync(userId))
            .ReturnsAsync(user);
        _jwtTokenServiceMock.Setup(x => x.GenerateJwtToken(user))
            .Returns(token);

        // Act
        var result = await _sut.AuthenticateAsync(request.Email, request.Password);

        // Assert
        result.Should().NotBeNull();
        result.Token.Should().Be(token);
        result.User.Should().NotBeNull();
        result.User.Email.Should().Be(user.Email);
    }
}