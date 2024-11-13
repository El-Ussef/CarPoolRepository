using AutoMapper;
using Identity.Core.Application.DTOs;
using Identity.Core.Domain.Common;
using Identity.Core.Domain.Entities;

namespace Identity.Core.Application.Mappings;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        // Base User to ApplicationUser mapping
        CreateMap<User, UserDto>();
        CreateMap<RegisterDriverRequest, Driver>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.CreationDate, opt => opt.Ignore())
            .ForMember(d => d.ModifiedDate, opt => opt.Ignore())
            .ForMember(d => d.Type, opt => opt.MapFrom(_ => Enums.UserType.Driver))
            .ForMember(d => d.CreatedAt, opt => opt.Ignore())    // Add this
            .ForMember(d => d.ModifiedAt, opt => opt.Ignore())   // Add this
            .ConstructUsing((src, _) => new Driver(
                src.Email,
                src.FirstName,
                src.LastName,
                src.PhoneNumber,
                src.DateOfBirth,
                src.Gender,
                Enums.UserType.Driver,
                src.ProfilePictureUrl,
                src.CarBrand,
                src.CarColor,
                src.CarNumber
            ));

        CreateMap<RegisterClientRequest, Client>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.CreationDate, opt => opt.Ignore())
            .ForMember(d => d.ModifiedDate, opt => opt.Ignore())
            .ForMember(d => d.Type, opt => opt.MapFrom(_ => Enums.UserType.Client))
            .ForMember(d => d.CreatedAt, opt => opt.Ignore())    // Add this
            .ForMember(d => d.ModifiedAt, opt => opt.Ignore())   // Add this
            .ConstructUsing((src, _) => new Client(
                src.Email,
                src.FirstName,
                src.LastName,
                src.PhoneNumber,
                src.DateOfBirth,
                src.Gender,
                Enums.UserType.Client,
                src.ProfilePictureUrl
            ));

        CreateMap<UserDto, User>()
            .ConstructUsing((src, ctx) =>
            {
                return src.Type switch
                {
                    "Driver" => new Driver(
                        src.Email,
                        src.FirstName,
                        src.LastName,
                        src.PhoneNumber ?? string.Empty,
                        src.DateOfBirth,
                        Enum.Parse<Enums.Gender>(src.Gender),
                        Enums.UserType.Driver,
                        src.ProfilePictureUrl,
                        src.CarBrand ?? string.Empty,
                        src.CarColor ?? string.Empty,
                        src.CarNumber ?? string.Empty
                    ),
                    "Client" => new Client(
                        src.Email,
                        src.FirstName,
                        src.LastName,
                        src.PhoneNumber ?? string.Empty,
                        src.DateOfBirth,
                        Enum.Parse<Enums.Gender>(src.Gender),
                        Enums.UserType.Client,
                        src.ProfilePictureUrl
                    ),
                    _ => throw new ArgumentException($"Unknown user type: {src.Type}")
                };
            });

        CreateMap<User, UserDto>()
            .ForMember(d => d.Gender, opt => opt.MapFrom(s => s.Gender.ToString()))
            .ForMember(d => d.Type, opt => opt.MapFrom(s => s.Type.ToString()));

        CreateMap<Driver, UserDto>()
            .IncludeBase<User, UserDto>()
            .ForMember(d => d.CarBrand, opt => opt.MapFrom(s => s.CarBrand))
            .ForMember(d => d.CarColor, opt => opt.MapFrom(s => s.CarColor))
            .ForMember(d => d.CarNumber, opt => opt.MapFrom(s => s.CarNumber));

        CreateMap<Client, UserDto>()
            .IncludeBase<User, UserDto>();
    }
}