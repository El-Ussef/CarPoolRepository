using Travels.Core.Application.Contracts;
using Travels.Core.Application.DTOs;
using Travels.Core.Domain.Entities;

namespace Travels.Infrastructure.Repositories;

public class TravelRepository : ITravelRepository
{
    public Task CreateTravel(Travel travel)
    {
        throw new NotImplementedException();
    }

    public Task<Travel> GetTravel(TravelSearchResponse searchResponse)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTravel(UpdateTravel travelRequest)
    {
        throw new NotImplementedException();
    }

    public Task ChooseTravel(Guid id)
    {
        throw new NotImplementedException();
    }
}