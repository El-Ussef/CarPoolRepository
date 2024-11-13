namespace CarPool.Domain.Entities;

public class Driver : User
{
    public required string DriverLicenseNumber { get; set; }

    // Navigation Properties
    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    public virtual ICollection<Ride> OfferedRides { get; set; } = new List<Ride>();

    // Methods
    public void AddVehicle(Vehicle vehicle)
    {
        vehicle.Owner = this;
        Vehicles.Add(vehicle);
    }

    public void OfferRide(Ride ride)
    {
        ride.Driver = this;
        OfferedRides.Add(ride);
        // Save ride to the database
    }
}
