namespace Identity.Core.Application.IntegrationEvents;

public record DriverCreatedEvent
{
    public Guid DriverId { get; init; }
    public string Email { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string PhoneNumber { get; init; }
    public DateTime? DateOfBirth { get; init; }
    public string Gender { get; init; }
    public string CarBrand { get; init; }
    public string CarColor { get; init; }
    public string CarNumber { get; init; }
}