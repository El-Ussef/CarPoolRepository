using Identity.Core.Domain.Entities;

namespace Identity.Core.Application.DTOs;

public class AuthenticationResult
{
    public UserDto User { get; set; }

    public string Token { get; set; }
}

public class UserDto
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string Type { get; set; }
    public string ProfilePictureUrl { get; set; }
    public string CarBrand { get; set; }
    public string CarColor { get; set; }
    public string CarNumber { get; set; }
}