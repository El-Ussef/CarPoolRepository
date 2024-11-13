using System.ComponentModel.DataAnnotations;
using Enum = CarPool.Shared.Events.Enums.Enum;

namespace CarPool.Shared.Events.Entities;

public class Ride
{
    public Guid RideId { get; set; } = Guid.NewGuid();

    // Foreign Keys
    public Guid DriverId { get; set; }
    public Guid VehicleId { get; set; }

    // Navigation Properties
    [Required]
    public virtual Driver Driver { get; set; }
    [Required]
    public virtual Vehicle Vehicle { get; set; }

    [Required]
    public Location DepartureLocation { get; set; }
    [Required]
    public Location ArrivalLocation { get; set; }
    [Required]
    public DateTime DepartureTime { get; set; }
    public DateTime? ArrivalTime { get; set; }
    [Required]
    public decimal PricePerSeat { get; set; }
    public int SeatsAvailable { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    // Methods
    public void AddPassenger(Passenger passenger)
    {
        // if (SeatsAvailable > 0)
        // {
        //     var booking = new Booking
        //     {
        //         Ride = this,
        //         Passenger = passenger,
        //         BookingStatus = Enum.BookingStatus.Confirmed,
        //         BookingDate = DateTime.Now
        //     };
        //     Bookings.Add(booking);
        //     SeatsAvailable--;
        //     // Save changes to the database
        // }
        // else
        // {
        //     throw new InvalidOperationException("No seats available.");
        // }
    }

    public void RemovePassenger(Passenger passenger)
    {
        var booking = Bookings.FirstOrDefault(b => b.Passenger.UserId == passenger.UserId);
        if (booking != null)
        {
            Bookings.Remove(booking);
            SeatsAvailable++;
            // Save changes to the database
        }
    }

    public void UpdateRideDetails() { /* Implement update logic */ }
}
