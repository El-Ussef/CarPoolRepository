using Enum = CarPool.Shared.Events.Enums.Enum;

namespace CarPool.Shared.Events.Entities;

public class Payment
{
    public Guid PaymentId { get; set; } = Guid.NewGuid();

    // Foreign Key
    public Guid BookingId { get; set; }

    // Navigation Property
    public  virtual required Booking Booking { get; set; }
    
    public required decimal Amount { get; set; }

    public required DateTime PaymentDate { get; set; }

    public required Enum.PaymentMethod PaymentMethod { get; set; }

    public required Enum.PaymentStatus PaymentStatus { get; set; }
}