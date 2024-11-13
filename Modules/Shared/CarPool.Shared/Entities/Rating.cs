using System.ComponentModel.DataAnnotations;

namespace CarPool.Shared.Events.Entities;

public class Rating
{
    public Guid RatingId { get; set; } = Guid.NewGuid();

    // Foreign Keys
    public Guid FromUserId { get; set; }
    public Guid ToUserId { get; set; }

    // Navigation Properties
    public virtual required User FromUser { get; set; }
    public virtual required User ToUser { get; set; }

    [Range(1, 5)]
    public int Score { get; set; } // From 1 to 5
    public string Comment { get; set; }

    public required DateTime Date { get; set; } = DateTime.Now;
}
