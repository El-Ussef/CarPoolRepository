using Identity.Core.Domain.Common;

namespace Identity.Core.Application.DTOs;

// public class RegisterUserRequest
// {
//     public required string Email { get;  set; }
//     public required string Password { get;  set; }
//     public required string FirstName { get;  set; }
//     public required string LastName { get;  set; }
//     public required string PhoneNumber { get;  set; }
//     
//     public required DateTime? DateOfBirth { get;  set; }
//     
//     public required Enums.Gender Gender { get;  set; }
//     
//     public required Enums.UserType Type { get;  set; }
//     
//     
//     public required string ProfilePictureUrl { get;  set; }
//     
//     public required string CarBrand { get;  set; }
//     
//     public required string CarColor { get;  set; }
//     
//     public required string CarNumber { get;  set; }
// }

public record RegisterDriverRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string PhoneNumber,
    DateTime DateOfBirth,
    Enums.Gender Gender,
    string? ProfilePictureUrl,
    string CarBrand,
    string CarColor,
    string CarNumber
);

public record RegisterClientRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string PhoneNumber,
    DateTime DateOfBirth,
    Enums.Gender Gender,
    string? ProfilePictureUrl
);

public record LoginRequest(
    string Email,
    string Password
);

public record UpdateProfileRequest(
    string FirstName,
    string LastName,
    string PhoneNumber,
    DateTime DateOfBirth,
    Enums.Gender Gender,
    string? ProfilePictureUrl
);