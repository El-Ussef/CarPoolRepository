using Identity.Core.Application.DTOs;

namespace Identity.Core.Application.Contracts;

public interface IAuthenticationService
{
    Task<AuthenticationResult> AuthenticateAsync(string email, string password);
}