using AutoMapper;
using Travels.Core.Application.DTOs;
using Travels.Core.Domain.Entities;

namespace Travels.Core.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateTravelRequest, Travel>();
        CreateMap<Travel, TravelResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
    }
}