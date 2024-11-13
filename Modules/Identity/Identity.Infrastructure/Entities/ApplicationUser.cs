using AutoMapper;
using Identity.Core.Application.Mappings;
using Identity.Core.Domain.Common;
using Identity.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identity.Infrastructure.Entities;

public class ApplicationUser : IdentityUser,IMapFrom<User>,IMapTo<User>,ITimeModification
{
    public string Email { get;  set; }

    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public DateTime? DateOfBirth { get; set; }
    
    public Enums.Gender Gender { get; set; }
    
    public Enums.UserType Type { get;  set; }
    
    public string? ProfilePictureUrl { get; set; }

    public string? CarBrand { get;  set; }
    
    public string? CarColor { get; set; }
    
    public string? CarNumber { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    
    // public virtual void Mapping(Profile profile)
    // {
    //     profile.CreateMap<ApplicationUser, User>();
    //     profile.CreateMap<User,ApplicationUser>();
    // }


}