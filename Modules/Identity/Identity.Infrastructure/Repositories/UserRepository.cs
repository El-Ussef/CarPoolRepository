using AutoMapper;
using Identity.Core.Application.Commons;
using Identity.Core.Application.Contracts;
using Identity.Core.Domain.Entities;
using Identity.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public UserRepository(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<Guid?> AuthenticateAsync(string email, string password)
    {
        var user = await _userManager.FindByNameAsync(email);
        if (user != null && await _userManager.CheckPasswordAsync(user, password))
        {
            return Guid.Parse(user.Id);
        }

        return null;
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        var appUser = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == id.ToString());
        return _mapper.Map<User>(appUser);
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        var appUser = await _userManager.FindByEmailAsync(email);
        return _mapper.Map<User>(appUser);
    }

    public async Task<Result> CreateAsync(User user,string password)
    {
        var appUser = _mapper.Map<ApplicationUser>(user);
        var result = await _userManager.CreateAsync(appUser,password);
        
        if (!result.Succeeded)
        {
            return Result.Failed(result.Errors.Select(x=> new Error
            {
                Code = x.Code,
                Description = x.Description
            }).ToArray());
        }
        return Result.Success;
    }

    public async Task UpdateAsync(User user)
    {
        var appUser = await _userManager.FindByIdAsync(user.Id.ToString());
        if (appUser == null)
        {
            // Handle not found (e.g., throw domain exception)
            throw new Exception("User not found");
        }

        // Map domain user properties to application user
        _mapper.Map(user, appUser);

        var result = await _userManager.UpdateAsync(appUser);

        if (!result.Succeeded)
        {
            // Handle errors
            throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
        }
    }
}