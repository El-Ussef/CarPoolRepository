namespace CarPool.Shared.Events.Interfaces;

public interface ICurrentUserService
{
    public string UserId { get; set; }
    
    public string Id { get; set; }
    public string UserName { get; set; }
    public string UserType { get; set; }

    public bool IsAuthenticated { get; set; }
    //List<string> RoleNames { get; }
}