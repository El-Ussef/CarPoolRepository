namespace Travel.Core.Domain.Entities;

public class Driver
{
    public Guid DriverId { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CarBrand { get; set; }
    public string CarColor { get; set; }
    public string CarNumber { get; set; }
}