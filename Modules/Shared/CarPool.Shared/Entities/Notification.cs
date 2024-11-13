namespace CarPool.Shared.Events.Entities;

public class Notification
{
    public Guid NotificationId { get; set; } = Guid.NewGuid();

    // Foreign Key
    public Guid UserId { get; set; }

    // Navigation Property
    public virtual required User User { get; set; }
    
    public required string Message { get; set; }
    public bool IsRead { get; set; } = false;

    public required DateTime CreatedDate { get; set; } = DateTime.Now;

    // Methods
    public void MarkAsRead()
    {
        IsRead = true;
        // Save changes to the database
    }
}
