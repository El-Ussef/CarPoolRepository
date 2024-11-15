namespace BookingTravels.Core.Application.DTOs;

public record MoneyDto
{
    public decimal Amount { get; set; }
    public string Currency { get; set; }
}