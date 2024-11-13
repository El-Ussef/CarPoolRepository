using AutoMapper;
using Identity.Core.Domain.Common;
using Identity.Core.Domain.Entities;
using Identity.Infrastructure.Entities;

namespace Identity.Infrastructure.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Base User to ApplicationUser mapping
        CreateMap<User, ApplicationUser>()
            .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.NormalizedUserName, opt => opt.MapFrom(s => s.Email.ToUpper()))
            .ForMember(d => d.NormalizedEmail, opt => opt.MapFrom(s => s.Email.ToUpper()))
            .ForMember(d => d.EmailConfirmed, opt => opt.MapFrom(_ => true))
            .ForMember(d => d.PhoneNumberConfirmed, opt => opt.MapFrom(_ => true))
            .ForMember(d => d.TwoFactorEnabled, opt => opt.MapFrom(_ => false))
            .ForMember(d => d.LockoutEnabled, opt => opt.MapFrom(_ => false))
            .ForMember(d => d.AccessFailedCount, opt => opt.MapFrom(_ => 0))
            .ForMember(d => d.PasswordHash, opt => opt.Ignore())
            .ForMember(d => d.SecurityStamp, opt => opt.Ignore())
            .ForMember(d => d.ConcurrencyStamp, opt => opt.Ignore())
            .ForMember(d => d.LockoutEnd, opt => opt.Ignore())
            .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => s.CreationDate))
            .ForMember(d => d.ModifiedAt, opt => opt.MapFrom(s => s.ModifiedDate))
            .Include<Driver, ApplicationUser>()
            .Include<Client, ApplicationUser>();

        CreateMap<Driver, ApplicationUser>()
            .IncludeBase<User, ApplicationUser>()
            .ForMember(d => d.Type, opt => opt.MapFrom(_ => Enums.UserType.Driver))
            .ForMember(d => d.CarBrand, opt => opt.MapFrom(s => s.CarBrand))
            .ForMember(d => d.CarColor, opt => opt.MapFrom(s => s.CarColor))
            .ForMember(d => d.CarNumber, opt => opt.MapFrom(s => s.CarNumber));

        CreateMap<Client, ApplicationUser>()
            .IncludeBase<User, ApplicationUser>()
            .ForMember(d => d.Type, opt => opt.MapFrom(_ => Enums.UserType.Client));

        // Base mapping for reverse direction
        // CreateMap<ApplicationUser, User>()
        //     .ForMember(d => d.Id, opt => opt.MapFrom(s => Guid.Parse(s.Id)))
        //     .ForMember(d => d.CreationDate, opt => opt.MapFrom(s => s.CreatedAt))
        //     .ForMember(d => d.ModifiedDate, opt => opt.MapFrom(s => s.ModifiedAt))
        //     .Include<ApplicationUser, Driver>()
        //     .Include<ApplicationUser, Client>();

        CreateMap<ApplicationUser, User>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => Guid.Parse(s.Id)))
            .ForMember(d => d.CreationDate, opt => opt.MapFrom(s => s.CreatedAt))
            .ForMember(d => d.ModifiedDate, opt => opt.MapFrom(s => s.ModifiedAt))
            .Include<ApplicationUser, Driver>()
            .Include<ApplicationUser, Client>()
            .ConstructUsing((src, ctx) =>
            {
                return src.Type switch
                {
                    Enums.UserType.Driver => ctx.Mapper.Map<Driver>(src),
                    Enums.UserType.Client => ctx.Mapper.Map<Client>(src),
                    _ => throw new ArgumentException($"Unknown user type: {src.Type}")
                };
            });

        CreateMap<ApplicationUser, Driver>()
            .IncludeBase<ApplicationUser, User>()
            .ConstructUsing((src, _) => new Driver(
                src.Email,
                src.FirstName,
                src.LastName,
                src.PhoneNumber ?? string.Empty,
                src.DateOfBirth ?? DateTime.MinValue,
                src.Gender,
                src.Type,
                src.ProfilePictureUrl,
                src.CarBrand ?? string.Empty,
                src.CarColor ?? string.Empty,
                src.CarNumber ?? string.Empty
            ));

        CreateMap<ApplicationUser, Client>()
            .IncludeBase<ApplicationUser, User>()
            .ConstructUsing((src, _) => new Client(
                src.Email,
                src.FirstName,
                src.LastName,
                src.PhoneNumber ?? string.Empty,
                src.DateOfBirth ?? DateTime.MinValue,
                src.Gender,
                src.Type,
                src.ProfilePictureUrl
            ));
        // CreateMap<ApplicationUser, Driver>()
        //     .IncludeBase<ApplicationUser, User>();

        CreateMap<ApplicationUser, Client>()
            .IncludeBase<ApplicationUser, User>();
    }
}