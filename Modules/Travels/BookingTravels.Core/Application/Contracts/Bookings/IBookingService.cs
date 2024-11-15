using BookingTravels.Core.Domain.Entities;

namespace BookingTravels.Core.Application.Contracts.Bookings;

public interface IBookingService
{
    Task<Booking> CreateBookingAsync(Guid travelId, Guid clientId, int seats);
    Task CancelBookingAsync(Guid bookingId, Guid clientId);
    Task<Booking> GetBookingByIdAsync(Guid bookingId);
    Task<IEnumerable<Booking>> GetBookingsByClientIdAsync(Guid clientId);
}