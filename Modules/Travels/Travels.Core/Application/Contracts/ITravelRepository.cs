using Travels.Core.Application.DTOs;
using Travels.Core.Domain.Entities;

namespace Travels.Core.Application.Contracts;

public interface ITravelRepository
{
    Task CreateTravel(Travel travel);

    Task<Travel> GetTravel(TravelSearchResponse searchResponse);

    Task UpdateTravel(UpdateTravel travelRequest);
    
    Task ChooseTravel(Guid id);
}