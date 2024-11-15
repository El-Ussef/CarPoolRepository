namespace BookingTravels.Core.Application.DTOs.Travels;

public record TravelResponse
{
    public Guid TravelId { get; set; }
    public Guid DriverId { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public DateTime DepartureTime { get; set; }
    public int AvailableSeats { get; set; }
    public decimal PricePerSeat { get; set; }
    public string Status { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}