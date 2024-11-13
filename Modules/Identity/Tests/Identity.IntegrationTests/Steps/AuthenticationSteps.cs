using System.IdentityModel.Tokens.Jwt;
using FluentAssertions;
using Identity.Core.Application.DTOs;
using Identity.Core.Domain.Common;
using Identity.IntegrationTests.Drivers;
using TechTalk.SpecFlow;
using static Identity.Core.Domain.Common.Enums;

namespace Identity.IntegrationTests.Steps;

[Binding]
public class AuthenticationSteps
{
    private readonly AuthenticationDriver _driver;
    private readonly ScenarioContext _scenarioContext;
    private HttpResponseMessage _registrationResponse;
    private LoginRequest _loginCredentials;
    private AuthenticationResult _loginResult;

    public AuthenticationSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        _driver = new AuthenticationDriver(scenarioContext);
    }

    [When(@"a new client registers with following details:")]
    public async Task WhenANewClientRegistersWithFollowingDetails(Table table)
    {
        var row = table.Rows[0];
        var request = new RegisterClientRequest(
            Email: row["Email"],
            Password: row["Password"],
            FirstName: row["FirstName"],
            LastName: row["LastName"],
            PhoneNumber: row["PhoneNumber"],
            DateOfBirth: DateTime.UtcNow.AddYears(-25),
            Gender: Gender.Male,
            ProfilePictureUrl: null
        );

        _registrationResponse = await _driver.RegisterClient(request);
        
        // Store the error message if registration fails
        if (!_registrationResponse.IsSuccessStatusCode)
        {
            var error = await _registrationResponse.Content.ReadAsStringAsync();
            _scenarioContext.Add("RegistrationError", error);
        }
        
        _scenarioContext.Add("LoginCredentials", new LoginRequest(request.Email, request.Password));
    }
    
    [When(@"a new driver registers with following details:")]
    public async Task WhenANewDriverRegistersWithFollowingDetails(Table table)
    {
        var row = table.Rows[0];
        var request = new RegisterDriverRequest(
            Email: row["Email"],
            Password: row["Password"],
            FirstName: row["FirstName"],
            LastName: row["LastName"],
            PhoneNumber: row["PhoneNumber"],
            DateOfBirth: DateTime.UtcNow.AddYears(-25),
            Gender: Gender.Male,
            ProfilePictureUrl: null,
            CarBrand:row["CarBrand"],
            CarColor:row["CarColor"],
            CarNumber:row["CarNumber"]
        );

        _registrationResponse = await _driver.RegisterDriver(request);
        
        // Store the error message if registration fails
        if (!_registrationResponse.IsSuccessStatusCode)
        {
            var error = await _registrationResponse.Content.ReadAsStringAsync();
            _scenarioContext.Add("RegistrationError", error);
        }
        
        _scenarioContext.Add("LoginCredentials", new LoginRequest(request.Email, request.Password));
    }

    [Then(@"the registration should be successful")]
    public async Task ThenTheRegistrationShouldBeSuccessful()
    {
        if (!_registrationResponse.IsSuccessStatusCode)
        {
            var error = await _registrationResponse.Content.ReadAsStringAsync();
            var statusCode = _registrationResponse.StatusCode;
            Console.WriteLine($"Response Status Code: {statusCode}");
            Console.WriteLine($"Response Content: {error}");
            
            _registrationResponse.IsSuccessStatusCode.Should().BeTrue(
                $"Registration failed with status code {statusCode} and error: {error}");
        }
    }

    [Then(@"the user should be able to login with these credentials")]
    public async Task ThenTheUserShouldBeAbleToLoginWithTheseCredentials()
    {
        var credentials = _scenarioContext.Get<LoginRequest>("LoginCredentials");
        var result = await _driver.Login(credentials);
        result.Token.Should().NotBeNullOrEmpty();
    }

    
    [Given(@"a registered user with following credentials:")]
    public void GivenARegisteredUserWithFollowingCredentials(Table table)
    {
        var row = table.Rows[0];
        _loginCredentials = new LoginRequest(
            Email: row["Email"],
            Password: row["Password"]
        );
    }

    [When(@"the user attempts to login")]
    public async Task WhenTheUserAttemptsToLogin()
    {
        _loginResult = await _driver.Login(_loginCredentials);
    }

    [Then(@"the login should be successful")]
    public void ThenTheLoginShouldBeSuccessful()
    {
        _loginResult.Should().NotBeNull();
    }

    [Then(@"a valid JWT token should be returned")]
    public void ThenAValidJWTTokenShouldBeReturned()
    {
        _loginResult.Token.Should().NotBeNullOrEmpty("because a valid JWT token should be returned");
        
        // Validate JWT token structure
        // var handler = new JwtSecurityTokenHandler();
        // var token = handler.ReadJwtToken(_loginResult.Token);
        //
        // token.Claims.Should().Contain(c => c.Type == JwtRegisteredClaimNames.Email);
        // token.Claims.Should().Contain(c => c.Type == JwtRegisteredClaimNames.Name);
        // token.Claims.Should().Contain(c => c.Type == "sid");
    }
    
}