using BookingTravels.Core.Application.DTOs.Travels;
using BookingTravels.Core.Domain.Entities;

namespace BookingTravels.Core.Application.Contracts.Travels;

public interface ITravelRepository
{
    Task CreateTravelAsync(Travel travel);

    Task<Travel?> GetByIdAsync(Guid id);
    
    Task<IReadOnlyList<Travel>> GetTravelByCriteriaAsync(TravelSearchRequest searchResponse);

    Task UpdateAsync(Travel travel);
    
}