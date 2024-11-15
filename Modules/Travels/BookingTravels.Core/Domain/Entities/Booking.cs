using BookingTravels.Core.Domain.ValueObjects;
using CarPool.Shared.Events.Common.Entities;
using CarPool.Shared.Events.Exceptions;
using Enum = CarPool.Shared.Events.Enums.Enum;

namespace BookingTravels.Core.Domain.Entities;

public class Booking : BaseEntity<Guid>
{
    public Guid TravelId { get; private set; }
    public Guid ClientId { get; private set; }
    public int SeatsBooked { get; private set; }
    public Money TotalPrice { get; private set; }
    public Enum.BookingStatus Status { get; private set; }
    public DateTime BookingDate { get; private set; }
    public DateTime? CancellationDate { get; private set; }

    // Navigation Property
    public Travel Travel { get; private set; }

    // Constructor
    public Booking(Guid travelId, Guid clientId, int seatsBooked, Money totalPrice)
    {
        Id = Guid.NewGuid();
        TravelId = travelId;
        ClientId = clientId;
        SeatsBooked = seatsBooked;
        TotalPrice = totalPrice;
        Status = Enum.BookingStatus.Pending;
        BookingDate = DateTime.UtcNow;
    }

    // For EF Core
    private Booking() { }

    // Business Logic
    public void ConfirmBooking()
    {
        if (Status != Enum.BookingStatus.Pending)
            throw new DomainException("Only pending bookings can be confirmed.");

        Status = Enum.BookingStatus.Confirmed;
    }

    public void CancelBooking()
    {
        if (Status == Enum.BookingStatus.Cancelled)
            throw new DomainException("Booking is already cancelled.");

        Status = Enum.BookingStatus.Cancelled;
        CancellationDate = DateTime.UtcNow;
    }
}
