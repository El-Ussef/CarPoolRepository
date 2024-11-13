namespace CarPool.Shared.Events.Entities;

public class Vehicle
{
    public Guid VehicleId { get; set; } = Guid.NewGuid();

    public required string Make { get; set; } // e.g., Toyota

    public required string Model { get; set; } // e.g., Corolla
    public int Year { get; set; }

    public required string LicensePlate { get; set; }
    public string Color { get; set; }
    public int SeatsAvailable { get; set; }

    // Foreign Key
    public Guid OwnerId { get; set; }
    // Navigation Property
    public virtual Driver Owner { get; set; }

    // Methods
    public void UpdateVehicleDetails() { /* Implement update logic */ }
}
