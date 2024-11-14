namespace Travels.Core.Application.DTOs;

public record UpdateTravel
{
    public string Origin { get; set; }
    public string Destination { get; set; }
    public DateTime? DepartureDate { get; set; }
    public int? SeatsNeeded { get; set; }
    public decimal? MaxPrice { get; set; }
}