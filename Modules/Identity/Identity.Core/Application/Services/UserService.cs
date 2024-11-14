using AutoMapper;
using CarPool.Shared.Events.Common.Interfaces;
using Identity.Core.Application.Commons;
using Identity.Core.Application.Contracts;
using Identity.Core.Application.DTOs;
using Identity.Core.Application.IntegrationEvents;
using Identity.Core.Domain.Common;
using Identity.Core.Domain.Entities;
using Identity.Core.Domain.Factories;

namespace Identity.Core.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IEventPublisher _eventPublisher;

    public UserService(IUserRepository userRepository, IMapper mapper, IEventPublisher eventPublisher)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
    }

    public async Task<Result> RegisterClientAsync(RegisterClientRequest command)
    {

        var user = _mapper.Map<Client>(command);
        
        var result = await _userRepository.CreateAsync(user, command.Password);

        return result;
    }

    public async Task<Result> RegisterDriverAsync(RegisterDriverRequest command)
    {

        var driver = _mapper.Map<Driver>(command);
        
        var result = await _userRepository.CreateAsync(driver, command.Password);

        if (result.Succeeded)
        {

            await _eventPublisher.PublishAsync(new DriverCreatedEvent
            {
                DriverId = driver.Id,
                Email = driver.Email,
                FirstName = driver.FirstName,
                LastName = driver.LastName,
                PhoneNumber = driver.PhoneNumber,
                DateOfBirth = driver.DateOfBirth,
                Gender = driver.Gender.ToString(),
                CarBrand = driver.CarBrand,
                CarColor = driver.CarColor,
                CarNumber = driver.CarNumber
            });

        }
        return result;
    }

    public async Task<User> GetUserByIdAsync(Guid userId)
    {
        return await _userRepository.GetByIdAsync(userId);
    }

    public async Task<Result> UpdateDriverAsync(RegisterDriverRequest command)
    {

        var driver = _mapper.Map<Driver>(command);
        
        var result = await _userRepository.CreateAsync(driver, command.Password);

        if (result.Succeeded)
        {

            await _eventPublisher.PublishAsync(new DriverCreatedEvent
            {
                DriverId = driver.Id,
                Email = driver.Email,
                FirstName = driver.FirstName,
                LastName = driver.LastName,
                PhoneNumber = driver.PhoneNumber,
                DateOfBirth = driver.DateOfBirth,
                Gender = driver.Gender.ToString(),
                CarBrand = driver.CarBrand,
                CarColor = driver.CarColor,
                CarNumber = driver.CarNumber
            });

        }
        return result;
    }
    // Other methods (e.g., UpdateProfileAsync, AuthenticateAsync)
}
