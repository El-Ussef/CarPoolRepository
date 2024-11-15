using BookingTravels.Core.Application.Contracts;
using BookingTravels.Core.Application.Contracts.Travels;
using BookingTravels.Core.Application.DTOs;
using BookingTravels.Core.Application.DTOs.Travels;
using BookingTravels.Core.Domain.Entities;
using CarPool.Shared.Events.Exceptions;
using Enum = CarPool.Shared.Events.Enums.Enum;

namespace BookingTravels.Core.Application.Services;

public class TravelService : ITravelService
{
    private readonly ITravelRepository _travelRepository;

    public TravelService(ITravelRepository travelRepository)
    {
        _travelRepository = travelRepository;
    }

    public async Task<Travel> GetTravelByIdAsync(Guid travelId)
    {
        return await _travelRepository.GetByIdAsync(travelId);
    }

    //TODO: possible delete this method
    public async Task ReserveSeatsAsync(Guid travelId, int seats)
    {
        var travel = await _travelRepository.GetByIdAsync(travelId);
        if (travel == null || travel.Status != Enum.TravelStatus.Active)
            throw new DomainException("Travel not available.");

        if (travel.AvailableSeats < seats)
            throw new DomainException("Not enough available seats.");

        travel.ReserveSeats(seats);
        await _travelRepository.UpdateAsync(travel);
    }

    //TODO: possible delete this method
    public async Task ReleaseSeatsAsync(Guid travelId, int seats)
    {
        var travel = await _travelRepository.GetByIdAsync(travelId);
        
        if (travel is null)
            throw new Exception("Travel not found.");

        travel.ReleaseSeats(seats);
        await _travelRepository.UpdateAsync(travel);
    }
    
    public Task<TravelResponse> GetTravelByCriteria(TravelSearchRequest searchRequest)
    {
        throw new NotImplementedException();
    }

    public Task CreateTravel(CreateTravelRequest travelRequest)
    {
        throw new NotImplementedException();
    }

    public Task Update(UpdateTravel travelRequest)
    {
        throw new NotImplementedException();
    }
    
}