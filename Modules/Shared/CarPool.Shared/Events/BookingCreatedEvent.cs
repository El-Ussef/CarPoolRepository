namespace CarPool.Shared.Events.Events;

public class BookingCreatedEvent : IEvent
{
    public Guid BookingId { get; set; }
    public Guid TravelId { get; set; }
    public Guid ClientId { get; set; }
    public int SeatsBooked { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime BookingDate { get; set; }
    
    public Guid EventId { get; }
    
    public DateTime Timestamp { get; }

    public BookingCreatedEvent(Guid bookingId, Guid travelId, Guid clientId, int seatsBooked, decimal totalPrice,
        DateTime bookingDate)
    {
        EventId = Guid.NewGuid();
        Timestamp = DateTime.UtcNow;
        BookingId = bookingId;
        TravelId = travelId;
        ClientId = clientId;
        SeatsBooked = seatsBooked;
        TotalPrice = totalPrice;
        BookingDate = bookingDate;
    }

    
}