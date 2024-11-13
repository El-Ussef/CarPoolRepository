namespace CarPool.Shared.Events.Events;

public record DriverCreated : IEvent
{
    public Guid DriverId { get; init; }
    public string Email { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string PhoneNumber { get; init; }
    public string Gender { get; init; }
    public string CarBrand { get; init; }
    public string CarColor { get; init; }
    public string CarNumber { get; init; }

    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime Timestamp { get; } = DateTime.UtcNow;

    public Guid UserId { get; set; }

    // public UserRegisteredEvent(Guid userId, string email)
    // {
    //     UserId = userId;
    //     Email = email;
    // }
}