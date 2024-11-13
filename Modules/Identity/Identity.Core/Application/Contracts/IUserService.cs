using Identity.Core.Application.Commons;
using Identity.Core.Application.DTOs;
using Identity.Core.Domain.Entities;

namespace Identity.Core.Application.Contracts;

public interface IUserService
{
    Task<Result> RegisterClientAsync(RegisterClientRequest command);

    Task<Result> RegisterDriverAsync(RegisterDriverRequest command);

    Task<User> GetUserByIdAsync(Guid userId);
}