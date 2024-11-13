using Enum = CarPool.Domain.Enums.Enum;

namespace CarPool.Domain.Entities;

public class Passenger : User
{
    // Navigation Properties
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    // Methods
    public List<Ride> SearchRides(Location departure, Location arrival, DateTime date)
    {
        // Implement search logic (e.g., querying the database)
        return new List<Ride>();
    }

    public Booking BookRide(Ride ride)
    {
        var booking = new Booking
        {
            Ride = ride,
            Passenger = this,
            BookingStatus = Enum.BookingStatus.Pending,
            BookingDate = DateTime.Now
        };
        Bookings.Add(booking);
        // Save booking to the database
        return booking;
    }
}
