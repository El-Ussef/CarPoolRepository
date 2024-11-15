namespace CarPool.Shared.Events.Events;

public class PaymentProcessedEvent : IEvent
{
    public Guid BookingId { get; set; }
    public bool IsSuccessful { get; set; }
    public string TransactionId { get; set; }
    public Guid EventId { get; }
    public DateTime Timestamp { get; }
}