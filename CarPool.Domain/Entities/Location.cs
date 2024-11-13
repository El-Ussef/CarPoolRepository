using System.ComponentModel.DataAnnotations;

namespace CarPool.Domain.Entities;

public class Location
{
    public required double Latitude { get; set; }

    public required double Longitude { get; set; }

    public required string Address { get; set; }

    // Methods
    public override string ToString()
    {
        return Address;
    }
}
