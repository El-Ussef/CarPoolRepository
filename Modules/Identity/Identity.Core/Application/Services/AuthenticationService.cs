using AutoMapper;
using Identity.Core.Application.Contracts;
using Identity.Core.Application.DTOs;

namespace Identity.Core.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _jwtTokenGenerator;
    private readonly IMapper _mapper;


    public AuthenticationService(IUserRepository userRepository, IJwtTokenService jwtTokenGenerator, IMapper mapper)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _mapper = mapper;
    }

    public async Task<AuthenticationResult> AuthenticateAsync(string email, string password)
    {

        var userId = await _userRepository.AuthenticateAsync(email, password);

        if (userId is null)
            throw new Exception("Invalid credentials");

        var user = await _userRepository.GetByIdAsync(userId.Value);

        var userDto = _mapper.Map<UserDto>(user);
        
        var token = _jwtTokenGenerator.GenerateJwtToken(user);

        return new AuthenticationResult
        {
            User = userDto,
            Token = token
        };
    }
}