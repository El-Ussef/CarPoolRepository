using Travels.Core.Application.Contracts;
using Travels.Core.Application.DTOs;

namespace Travels.Core.Application.Services;

public class TravelService : ITravelService
{
    
    public Task<TravelResponse> GetTravelByCriteria(TravelSearchRequest searchRequest)
    {
        throw new NotImplementedException();
    }

    public Task CreateTravel(CreateTravelRequest travelRequest)
    {
        throw new NotImplementedException();
    }

    //Only for the creator of this travel
    public Task UpdateTravel(UpdateTravel travelRequest)
    {
        throw new NotImplementedException();
    }

    public Task ChooseTravel(Guid id)
    {
        throw new NotImplementedException();
    }
}