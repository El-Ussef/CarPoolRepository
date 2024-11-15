using BookingTravels.Core.Application.Contracts.Bookings;
using BookingTravels.Core.Domain.Entities;
using BookingTravels.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BookingTravels.Infrastructure.Repositories;

public class BookingRepository: IBookingRepository
{
    private readonly AppDbContext _context;

    public BookingRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Booking booking)
    {
        await _context.Bookings.AddAsync(booking);
        await _context.SaveChangesAsync();
    }

    public async Task<Booking?> GetByIdAsync(Guid bookingId)
    {
        return await _context.Bookings
            .Include(b => b.Travel)
            .ThenInclude(t => t.Driver)
            .FirstOrDefaultAsync(b => b.Id == bookingId);
    }

    public async Task<IEnumerable<Booking>> GetBookingsByClientIdAsync(Guid clientId)
    {
        return await _context.Bookings
            .Where(b => b.ClientId == clientId)
            .Include(b => b.Travel)
            .ThenInclude(t => t.Driver)
            .ToListAsync();
    }

    public async Task UpdateAsync(Booking booking)
    {
        _context.Bookings.Update(booking);
        await _context.SaveChangesAsync();
    }
}
