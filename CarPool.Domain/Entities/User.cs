using System.ComponentModel.DataAnnotations;

namespace CarPool.Domain.Entities;

public abstract class User
{
    public Guid UserId { get; set; } = Guid.NewGuid();
    [StringLength(50)]
    public required string FirstName { get; set; }

    [StringLength(50)]
    public required string LastName { get; set; }

    [EmailAddress]
    [StringLength(100)]
    public required string Email { get; set; } // Unique

    public required string PasswordHash { get; set; }
    [Phone]
    public string PhoneNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string Gender { get; set; }
    public byte[] ProfilePicture { get; set; }

    // Navigation Properties
    public virtual ICollection<Rating> RatingsReceived { get; set; } = new List<Rating>();
    public virtual ICollection<Rating> RatingsGiven { get; set; } = new List<Rating>();
    // public virtual ICollection<Message> MessagesSent { get; set; } = new List<Message>();
    // public virtual ICollection<Message> MessagesReceived { get; set; } = new List<Message>();
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    // Methods
    public void Register() { /* Implement registration logic */ }
    public void Login() { /* Implement login logic */ }
    public void Logout() { /* Implement logout logic */ }
    public void UpdateProfile() { /* Implement profile update logic */ }
}
