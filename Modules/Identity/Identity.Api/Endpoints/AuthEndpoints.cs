using Identity.Core.Application.Contracts;
using Identity.Core.Application.DTOs;
using Identity.Core.Domain.Common;
using Identity.Core.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using IAuthenticationService = Identity.Core.Application.Contracts.IAuthenticationService;


namespace Identity.Api.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth");

        // Registration endpoints
        group.MapPost("/register/driver", RegisterDriver);
        group.MapPost("/register/client", RegisterClient);
        
        // Authentication endpoints
        group.MapPost("/login", Login);
        group.MapPost("/logout", Logout).RequireAuthorization();
        
        // User management endpoints
        // group.MapGet("/me", GetCurrentUser).RequireAuthorization();
        // group.MapPut("/me", UpdateProfile).RequireAuthorization();
    }

    private static async Task<IResult> RegisterDriver(
        RegisterDriverRequest request,
        IUserService userRepository)
    {
        // var driver = new Driver(
        //     request.Email,
        //     request.FirstName,
        //     request.LastName,
        //     request.PhoneNumber,
        //     request.DateOfBirth,
        //     request.Gender,
        //     Enums.UserType.Driver,
        //     request.ProfilePictureUrl,
        //     request.CarBrand,
        //     request.CarColor,
        //     request.CarNumber
        //);

        var result = await userRepository.RegisterDriverAsync(request);
        return result.Succeeded ? Results.Ok() : Results.BadRequest(new { message = result.Errors});
    }

    private static async Task<IResult> RegisterClient(
        RegisterClientRequest request,
        IUserService userRepository)
    {
        // var client = new Client(
        //     request.Email,
        //     request.FirstName,
        //     request.LastName,
        //     request.PhoneNumber,
        //     request.DateOfBirth,
        //     request.Gender,
        //     Enums.UserType.Client,
        //     request.ProfilePictureUrl
        // );

        var result = await userRepository.RegisterClientAsync(request);
        return result.Succeeded ? Results.Ok() : Results.BadRequest(new { message = result.Errors});
    }

    private static async Task<IResult> Login(
        LoginRequest request,
        IUserService userRepository,
        IAuthenticationService authenticationService)
    {
        var result = await authenticationService.AuthenticateAsync(request.Email, request.Password);
        
        if (result is null)
            return Results.Unauthorized();
        
        return Results.Ok(new { data = result });
    }

    private static async Task<IResult> Logout(
        HttpContext context)
    {
        await context.SignOutAsync();
        return Results.Ok();
    }

    // private static async Task<IResult> GetCurrentUser(
    //     ICurrentUserService currentUserService,
    //     IUserRepository userRepository)
    // {
    //     var userId = currentUserService.UserId;
    //     if (!userId.HasValue)
    //         return Results.Unauthorized();
    //
    //     var user = await userRepository.GetByIdAsync(userId.Value);
    //     return user != null ? Results.Ok(user) : Results.NotFound();
    // }

    // private static async Task<IResult> UpdateProfile(
    //     UpdateProfileRequest request,
    //     ICurrentUserService currentUserService,
    //     IUserRepository userRepository)
    // {
    //     var userId = currentUserService.UserId;
    //     if (!userId.HasValue)
    //         return Results.Unauthorized();
    //
    //     var user = await userRepository.GetByIdAsync(userId.Value);
    //     if (user == null)
    //         return Results.NotFound();
    //
    //     // Update user properties
    //     user.UpdateProfile(
    //         request.FirstName,
    //         request.LastName,
    //         request.PhoneNumber,
    //         request.DateOfBirth,
    //         request.Gender,
    //         request.ProfilePictureUrl
    //     );
    //
    //     await userRepository.UpdateAsync(user);
    //     return Results.Ok();
    // }
}