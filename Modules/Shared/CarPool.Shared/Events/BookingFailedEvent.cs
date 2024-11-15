namespace CarPool.Shared.Events.Events;

public class BookingFailedEvent : IEvent
{
    public Guid BookingId { get; set; }
    public string Reason { get; set; }
    public Guid EventId { get; }
    public DateTime Timestamp { get; }
}