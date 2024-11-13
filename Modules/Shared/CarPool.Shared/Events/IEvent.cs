namespace CarPool.Shared.Events.Events;

public interface IEvent
{
    Guid EventId { get; }
    DateTime Timestamp { get; }
}