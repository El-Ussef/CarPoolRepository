using Identity.Core.Domain.Common;

namespace Identity.Core.Domain.Entities;

public class Driver : User
{
    public string CarBrand { get; private set; }
    
    public string CarColor { get; private set; }
    
    public string CarNumber { get; private set; }
    
    public Driver(string email, string firstName, string lastName, string phoneNumber, DateTime? dateOfBirth, Enums.Gender gender, Enums.UserType type, string profilePictureUrl, string carBrand, string carColor, string carNumber) 
        : base(email, firstName, lastName, phoneNumber, dateOfBirth, gender, type, profilePictureUrl)
    {
        CarBrand = carBrand;
        CarColor = carColor;
        CarNumber = carNumber;
    }
    
    public void UpdateDriver(
        string firstName,
        string lastName,
        string phoneNumber, 
        string profilePictureUrl, 
        string carBrand, 
        string carColor, 
        string carNumber)
    {
        base.UpdateProfile( firstName,
             lastName,
             phoneNumber, 
             profilePictureUrl
        );

        CarBrand = carBrand;
        CarColor = carColor;
        CarNumber = carNumber;
    }
}