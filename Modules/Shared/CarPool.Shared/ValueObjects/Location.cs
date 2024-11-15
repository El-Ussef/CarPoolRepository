using CarPool.Shared.Events.Exceptions;

namespace CarPool.Shared.Events.ValueObjects;

public record Location
{
    public double Latitude { get; }
    public double Longitude { get; }
    public string Address { get; }

    private Location(double latitude, double longitude, string address)
    {
        if (latitude < -90 || latitude > 90)
            throw new DomainException("Invalid latitude value");
            
        if (longitude < -180 || longitude > 180)
            throw new DomainException("Invalid longitude value");
            
        if (string.IsNullOrWhiteSpace(address))
            throw new DomainException("Address cannot be empty");

        Latitude = latitude;
        Longitude = longitude;
        Address = address;
    }

    public static Location Create(double latitude, double longitude, string address) 
        => new Location(latitude, longitude, address);

    public double CalculateDistanceTo(Location other)
    {
        var R = 6371; // Earth's radius in kilometers
        var dLat = ToRad(other.Latitude - Latitude);
        var dLon = ToRad(other.Longitude - Longitude);
        
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRad(Latitude)) * Math.Cos(ToRad(other.Latitude)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
                
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return R * c;
    }

    private static double ToRad(double degrees) => degrees * Math.PI / 180;

    public override string ToString() => $"{Address} ({Latitude}, {Longitude})";
}