using BookingTravels.Core.Application.Contracts;
using BookingTravels.Core.Application.Contracts.Travels;
using BookingTravels.Core.Application.DTOs;
using BookingTravels.Core.Application.DTOs.Travels;
using BookingTravels.Core.Domain.Entities;
using BookingTravels.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Enum = CarPool.Shared.Events.Enums.Enum;

namespace BookingTravels.Infrastructure.Repositories;

public class TravelRepository : ITravelRepository
{
    private readonly AppDbContext _dbContext;

    public TravelRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateTravelAsync(Travel travel)
    {
        await _dbContext.Travels.AddAsync(travel);
    }

    public async Task<Travel?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Travels.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IReadOnlyList<Travel>> GetTravelByCriteriaAsync(TravelSearchRequest request)
    {

        var query = _dbContext.Travels.AsQueryable().Where(t => t.Status == Enum.TravelStatus.Open);

        if (!string.IsNullOrWhiteSpace(request.OriginAddress))
        {
            query = query.Where(t =>
                EF.Functions.Like(t.Origin.Address.ToLower(), $"%{request.OriginAddress.ToLower()}%"));
        }

        if (!string.IsNullOrWhiteSpace(request.DestinationAddress))
        {
            query = query.Where(t =>
                EF.Functions.Like(t.Destination.Address.ToLower(), $"%{request.DestinationAddress.ToLower()}%"));
        }

        if (request.DepartureDate.HasValue)
        {
            var date = request.DepartureDate.Value.Date;
            query = query.Where(t => t.DepartureTime.Date == date);
        }

        if (request.SeatsNeeded.HasValue)
        {
            query = query.Where(t => t.AvailableSeats >= request.SeatsNeeded.Value);
        }

        if (request.MaxPrice.HasValue)
        {
            query = query.Where(t => t.PricePerSeat.Amount <= request.MaxPrice.Value);
        }

        switch (request.SortBy?.ToLower())
        {
            case "price":
                query = request.SortDescending
                    ? query.OrderByDescending(t => t.PricePerSeat)
                    : query.OrderBy(t => t.PricePerSeat);
                break;
            case "departuretime":
                query = request.SortDescending
                    ? query.OrderByDescending(t => t.DepartureTime)
                    : query.OrderBy(t => t.DepartureTime);
                break;
            default:
                query = query.OrderBy(t => t.DepartureTime);
                break;
        }
        
        query = query.Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize);
        
        return await query.ToListAsync();
    }
    
    
    public async Task UpdateAsync(Travel travel)
    {
        _dbContext.Travels.Update(travel);
        await _dbContext.SaveChangesAsync();
    }
}