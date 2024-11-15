namespace BookingTravels.Core.Application.DTOs.Bookings;

public record BookingCreateRequest
{
    public Guid TravelId { get; set; }
    public int Seats { get; set; }
}