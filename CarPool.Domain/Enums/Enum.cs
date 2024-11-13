namespace CarPool.Domain.Enums;

public class Enum
{
    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Cancelled
    }
    public enum PaymentMethod
    {
        CreditCard,
        PayPal,
        Stripe,
        Other
    }

    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed,
        Refunded
    }
}