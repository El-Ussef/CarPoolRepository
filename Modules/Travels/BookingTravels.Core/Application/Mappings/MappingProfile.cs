using AutoMapper;
using BookingTravels.Core.Application.DTOs;
using BookingTravels.Core.Application.DTOs.Bookings;
using BookingTravels.Core.Application.DTOs.Travels;
using BookingTravels.Core.Domain.Entities;

namespace BookingTravels.Core.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateTravelRequest, Travel>();
        CreateMap<Travel, TravelResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        
        CreateMap<BookingCreateRequest, Booking>()
            .ForMember(dest => dest.TotalPrice, opt => opt.Ignore()) // Calculated in service
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.BookingDate, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Booking, BookingResponse>()
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => new MoneyDto()
            {
                Amount = src.TotalPrice.Amount,
                Currency = src.TotalPrice.Currency
            }))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
    }
}