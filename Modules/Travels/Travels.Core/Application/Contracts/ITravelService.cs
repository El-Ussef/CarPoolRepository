using Travels.Core.Application.DTOs;

namespace Travels.Core.Application.Contracts;

public interface ITravelService
{
    Task<TravelResponse> GetTravelByCriteria(TravelSearchRequest searchRequest);

    Task CreateTravel(CreateTravelRequest travelRequest);

    Task UpdateTravel(UpdateTravel travelRequest);
    Task ChooseTravel(Guid id);
}