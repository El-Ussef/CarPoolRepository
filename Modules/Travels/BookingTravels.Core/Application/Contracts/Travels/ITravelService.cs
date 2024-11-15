using BookingTravels.Core.Application.DTOs.Travels;
using BookingTravels.Core.Domain.Entities;

namespace BookingTravels.Core.Application.Contracts.Travels;

public interface ITravelService
{
    Task<TravelResponse> GetTravelByCriteria(TravelSearchRequest searchRequest);

    Task CreateTravel(CreateTravelRequest travelRequest);

    Task Update(UpdateTravel travelRequest);
    
    Task<Travel> GetTravelByIdAsync(Guid id);

    Task ReleaseSeatsAsync(Guid travelId, int seats);

    Task ReserveSeatsAsync(Guid travelId, int seats);
}