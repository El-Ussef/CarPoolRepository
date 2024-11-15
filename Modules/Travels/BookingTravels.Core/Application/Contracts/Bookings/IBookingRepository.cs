using BookingTravels.Core.Domain.Entities;

namespace BookingTravels.Core.Application.Contracts.Bookings;

public interface IBookingRepository
{
    Task AddAsync(Booking booking);
    Task<Booking> GetByIdAsync(Guid bookingId);
    Task<IEnumerable<Booking>> GetBookingsByClientIdAsync(Guid clientId);
    Task UpdateAsync(Booking booking);
}