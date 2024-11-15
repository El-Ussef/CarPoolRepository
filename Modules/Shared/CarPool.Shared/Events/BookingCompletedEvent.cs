namespace CarPool.Shared.Events.Events;

public class BookingCompletedEvent : IEvent
{
    public Guid BookingId { get; set; }
    public Guid EventId { get; }
    public DateTime Timestamp { get; }
}