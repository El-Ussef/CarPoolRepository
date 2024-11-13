using CarPool.Shared.Events.Common.Entities;
using Identity.Core.Domain.Common;

namespace Identity.Core.Domain.Entities;

//IAggregateRoot
public abstract class User: BaseEntity<Guid>
{
    public User(string email,
        string firstName,
        string lastName,
        string phoneNumber,
        DateTime? dateOfBirth,
        Enums.Gender gender,
        Enums.UserType type,
        string? profilePictureUrl)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        Type = type;
        ProfilePictureUrl = profilePictureUrl;
        CreationDate = DateTime.UtcNow;
    }

    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string PhoneNumber { get; private set; }
    
    public DateTime? DateOfBirth { get; private set; }
    
    public Enums.Gender Gender { get; private set; }
    
    public Enums.UserType Type { get; private set; }
    public string? ProfilePictureUrl { get; private set; }
    

    
    /// <summary>
    /// Business/domain date
    /// </summary>
    public DateTime CreationDate { get; private set; }
    
    /// <summary>
    /// Business/domain date
    /// </summary>
    public DateTime ModifiedDate { get; private set; }

    // Additional domain-specific properties and methods

    // Constructor
    protected User() { } // For EF Core

    // Business logic methods
    public void UpdateProfile(
        string firstName,
        string lastName,
        string phoneNumber, 
        string profilePictureUrl)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        ProfilePictureUrl = profilePictureUrl;
        ModifiedDate = DateTime.UtcNow;
    }
}