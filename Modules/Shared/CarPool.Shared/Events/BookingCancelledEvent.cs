namespace CarPool.Shared.Events.Events;

public class BookingCancelledEvent : IEvent
{
    public Guid BookingId { get; set; }
    public Guid TravelId { get; set; }
    public Guid ClientId { get; set; }
    public int SeatsCancelled { get; set; }
    public DateTime CancellationDate { get; set; }
    
    public Guid EventId { get; }
    
    public DateTime Timestamp { get; }

    public BookingCancelledEvent(Guid bookingId, Guid travelId, Guid clientId, int seatsCancelled, DateTime cancellationDate)
    {
        EventId = Guid.NewGuid();
        Timestamp = DateTime.UtcNow;
        BookingId = bookingId;
        TravelId = travelId;
        ClientId = clientId;
        SeatsCancelled = seatsCancelled;
        CancellationDate = cancellationDate;
    }

    
}