using Identity.Core.Domain.Common;

namespace Identity.Core.Domain.Entities;

public class Client : User
{
    public Client(string email, string firstName, string lastName, string phoneNumber, DateTime? dateOfBirth, Enums.Gender gender, Enums.UserType type, string profilePictureUrl) 
        : base(email, firstName, lastName, phoneNumber, dateOfBirth, gender, type, profilePictureUrl)
    {
    }

    public void UpdateClient(string firstName,
        string lastName,
        string phoneNumber, 
        string profilePictureUrl)
    {
        base.UpdateProfile( firstName,
            lastName,
            phoneNumber, 
            profilePictureUrl
        );
    }
}