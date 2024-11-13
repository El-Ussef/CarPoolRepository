using CarPool.Shared.Events.Common.Entities;
using Travel.Core.Domain.ValueObjects;
using Enum = CarPool.Shared.Events.Enums.Enum;

namespace Travel.Core.Domain.Entities;

public class Travel : BaseEntity<Guid>
{
    public Guid DriverId { get; set; }
    //public string Origin { get; set; } // i should make this a repo data 
    
    public Location Origin { get; set; } // i should make this a repo data 
    
    //public string Destination { get; set; } // i should make this a repo data 
    
    public Location Destination { get; set; } // i should make this a repo data 
    
    public DateTime DepartureTime { get; set; }
    
    public int AvailableSeats { get; set; }
    
    public decimal PricePerSeat { get; set; } //should this be of type Money ? 
    
    public Enum.TravelStatus Status { get; set; }
    
    public Driver Driver { get; set; }
    
}