using System.ComponentModel.DataAnnotations;

namespace BookingTravels.Core.Application.DTOs.Travels;

public record TravelSearchRequest
{
    public string OriginAddress { get; set; }
    public string DestinationAddress { get; set; }
    public DateTime? DepartureDate { get; set; }
    public int? SeatsNeeded { get; set; }
    public decimal? MaxPrice { get; set; }
    
    public int PageNumber { get; set; } = 1;

    [Range(1, 100, ErrorMessage = "Page size must be between 1 and 100.")]
    public int PageSize { get; set; } = 10;

    public string SortBy { get; set; } // e.g., "price", "departureTime"
    public bool SortDescending { get; set; } = false;
}