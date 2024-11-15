using BookingTravels.Core.Domain.ValueObjects;

namespace BookingTravels.Core.Application.DTOs.Travels;

public record CreateTravelRequest
{
    public Location Origin { get; set; }
    public Location Destination { get; set; }
    public DateTime DepartureTime { get; set; }
    public int AvailableSeats { get; set; }
    public decimal PricePerSeat { get; set; }
}