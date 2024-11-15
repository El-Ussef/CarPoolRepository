

namespace BookingTravels.Core.Application.DTOs.Bookings;

public record BookingResponse
{
    public Guid BookingId { get; set; }
    public Guid TravelId { get; set; }
    public Guid ClientId { get; set; }
    public int SeatsBooked { get; set; }
    public MoneyDto TotalPrice { get; set; }
    public string Status { get; set; }
    public DateTime BookingDate { get; set; }
    public DateTime? CancellationDate { get; set; }
}