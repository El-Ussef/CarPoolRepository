using Identity.Core.Application.Commons;
using Identity.Core.Domain.Entities;

namespace Identity.Core.Application.Contracts;

public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id);
    Task<User> GetByEmailAsync(string email);
    Task<Result> CreateAsync(User user,string password);
    Task UpdateAsync(User user);

    Task<Guid?> AuthenticateAsync(string email, string password);
}