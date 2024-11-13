using System.Reflection;
using AutoMapper;
using CarPool.Shared.Events.Interfaces;
using Identity.Core.Application.Contracts;
using Identity.Core.Application.DTOs;
using Identity.Core.Domain.Common;
using Identity.Core.Domain.Entities;
using Identity.Infrastructure.Context;
using Identity.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Identity.UnitTests;

public abstract class TestBase
{
    protected readonly IMapper Mapper;
    protected readonly Mock<ICurrentUserService> CurrentUserServiceMock;
    protected readonly Mock<IDateTimeService> DateTimeServiceMock;

    protected TestBase()
    {
        var mappingConfig = new MapperConfiguration(cfg =>
        {
            // Base User to ApplicationUser mapping
            cfg.CreateMap<User, ApplicationUser>()
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
                .ForMember(d => d.CarBrand, opt => opt.Ignore())
                .ForMember(d => d.CarColor, opt => opt.Ignore())
                .ForMember(d => d.CarNumber, opt => opt.Ignore())
                .Include<Driver, ApplicationUser>()
                .Include<Client, ApplicationUser>();

            cfg.CreateMap<ApplicationUser, User>()
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

            cfg.CreateMap<ApplicationUser, Driver>()
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

            cfg.CreateMap<ApplicationUser, Client>()
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
            // Specific mappings for Driver and Client
            cfg.CreateMap<Driver, ApplicationUser>()
                .IncludeBase<User, ApplicationUser>()
                .ForMember(d => d.Type, opt => opt.MapFrom(_ => Enums.UserType.Driver))
                .ForMember(d => d.CarBrand, opt => opt.MapFrom(s => s.CarBrand))
                .ForMember(d => d.CarColor, opt => opt.MapFrom(s => s.CarColor))
                .ForMember(d => d.CarNumber, opt => opt.MapFrom(s => s.CarNumber));

            cfg.CreateMap<Client, ApplicationUser>()
                .IncludeBase<User, ApplicationUser>()
                .ForMember(d => d.Type, opt => opt.MapFrom(_ => Enums.UserType.Client));

            cfg.CreateMap<RegisterDriverRequest, Driver>()
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

            cfg.CreateMap<RegisterClientRequest, Client>()
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


            cfg.CreateMap<User, UserDto>()
                .ForMember(d => d.Gender, opt => opt.MapFrom(s => s.Gender.ToString()))
                .ForMember(d => d.Type, opt => opt.MapFrom(s => s.Type.ToString()))
                .ForMember(d => d.CarBrand, opt => opt.Ignore())
                .ForMember(d => d.CarColor, opt => opt.Ignore())
                .ForMember(d => d.CarNumber, opt => opt.Ignore());

            cfg.CreateMap<Driver, UserDto>()
                .IncludeBase<User, UserDto>()
                .ForMember(d => d.CarBrand, opt => opt.MapFrom(s => s.CarBrand))
                .ForMember(d => d.CarColor, opt => opt.MapFrom(s => s.CarColor))
                .ForMember(d => d.CarNumber, opt => opt.MapFrom(s => s.CarNumber));

            cfg.CreateMap<Client, UserDto>()
                .IncludeBase<User, UserDto>()
                .ForMember(d => d.CarBrand, opt => opt.Ignore())
                .ForMember(d => d.CarColor, opt => opt.Ignore())
                .ForMember(d => d.CarNumber, opt => opt.Ignore());


        });

        mappingConfig.AssertConfigurationIsValid();
        Mapper = mappingConfig.CreateMapper();

        CurrentUserServiceMock = new Mock<ICurrentUserService>();
        DateTimeServiceMock = new Mock<IDateTimeService>();
    }
    

    protected AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(
            options,
            new ConfigurationBuilder().Build(),
            CurrentUserServiceMock.Object,
            DateTimeServiceMock.Object);
    }
}