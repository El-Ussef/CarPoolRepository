using System.Security.Claims;
using Identity.Core.Domain.Entities;

namespace Identity.Core.Application.Contracts;

public interface IJwtTokenService
{
    string GenerateJwtToken(User user);
    string GenerateJwtRefreshToken(User user);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}