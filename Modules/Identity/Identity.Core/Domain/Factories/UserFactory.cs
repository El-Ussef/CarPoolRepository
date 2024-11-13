using Identity.Core.Application.DTOs;
using Identity.Core.Domain.Common;
using Identity.Core.Domain.Entities;

namespace Identity.Core.Domain.Factories;

/// <summary>
/// to be deleted
/// </summary>
// public static class UserFactory
// {
//     public static User? CreateUser(RegisterUserRequest command)
//     {
//         switch (command.Type)
//         {
//             case Enums.UserType.Client:
//                 return new Client(
//                     command.Email,
//                     command.FirstName,
//                     command.LastName,
//                     command.PhoneNumber,
//                     command.DateOfBirth,
//                     command.Gender,
//                     command.Type,
//                     command.ProfilePictureUrl
//                 );
//             case Enums.UserType.Driver:
//                 return new Driver(
//                     command.Email,
//                     command.FirstName,
//                     command.LastName,
//                     command.PhoneNumber,
//                     command.DateOfBirth,
//                     command.Gender,
//                     command.Type,
//                     command.ProfilePictureUrl,
//                     command.CarBrand,
//                     command.CarColor,
//                     command.CarNumber
//                 );
//             default:
//                 return null;
//         }
//     }
// }