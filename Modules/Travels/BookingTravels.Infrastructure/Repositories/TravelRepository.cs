using BookingTravels.Core.Application.Contracts;
using BookingTravels.Core.Application.Contracts.Travels;
using BookingTravels.Core.Application.DTOs;
using BookingTravels.Core.Application.DTOs.Travels;
using BookingTravels.Core.Domain.Entities;

namespace BookingTravels.Infrastructure.Repositories;

public class TravelRepository : ITravelRepository
{
    public Task CreateTravelAsync(Travel travel)
    {
        throw new NotImplementedException();
    }

    public Task<Travel> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
    
    public Task<Travel> GetTravelByCriteriaAsync(TravelSearchResponse searchResponse)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Travel travelRequest)
    {
        throw new NotImplementedException();
    }

    public Task ChooseTravelAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}