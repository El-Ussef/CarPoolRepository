using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Identity.Core.Application.Contracts;

namespace Identity.Api.Services;

public class CurrentUserService : ICurrentUserService
{
private readonly IHttpContextAccessor _httpContextAccessor;
public string UserId { get; set; }
public string Id { get; set; }
//public Guid StorageAreaId { get; }
public string UserName { get; set; }
public string UserType { get; set; }
public List<string> RoleNames { get; set; }

public bool IsAuthenticated
{
    get => _httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    set => throw new NotImplementedException();
}

public CurrentUserService(IHttpContextAccessor httpContextAccessor)
{
    _httpContextAccessor = httpContextAccessor;
    //var zoneInfo = httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtClaimTypes.ZoneInfo);
    var user = httpContextAccessor.HttpContext?.User;
    if (user != null)
    {
        UserId = user.FindFirstValue("userid");
        UserName = user.FindFirstValue(JwtRegisteredClaimNames.Name);
        UserType = user.FindFirstValue("usertype");
        Id = user.FindFirstValue("sid");
    }
    // Assuming TenantId is stored as a custom claim named "tenantid"
}
}