namespace CarPool.Shared.Events.Events;

public class SeatsReservedEvent : IEvent
{
    public Guid BookingId { get; set; }
    public bool IsSuccessful { get; set; }
    public Guid EventId { get; }
    public DateTime Timestamp { get; }
}