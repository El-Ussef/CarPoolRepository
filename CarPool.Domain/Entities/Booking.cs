using System.ComponentModel.DataAnnotations;
using Enum = CarPool.Domain.Enums.Enum;
using CarPool.Domain.ValueObjects;
using CarPool.Domain.Exceptions;

namespace CarPool.Domain.Entities;

public class Booking
{
    public Guid BookingId { get; set; } = Guid.NewGuid();

    // Foreign Keys
    public Guid RideId { get; set; }
    public Guid PassengerId { get; set; }

    // Navigation Properties

    public virtual required Ride Ride { get; set; }
    [Required]
    public virtual required Passenger Passenger { get; set; }

    [Required]
    public required Enum.BookingStatus  BookingStatus { get; set; }
    [Required]
    public required DateTime BookingDate { get; set; }

    // Add validation and business rules
    private void ValidateBookingDate()
    {
        if (BookingDate > Ride.DepartureTime)
            throw new DomainException("Booking date cannot be after ride departure time");
    }

    // Add domain behavior
    public void UpdateStatus(Enum.BookingStatus newStatus)
    {
        if (BookingStatus == Enum.BookingStatus.Cancelled)
            throw new DomainException("Cannot update status of cancelled booking");
            
        BookingStatus = newStatus;
    }
    // Add money value object for payment
    public required Money Price { get; set; }

    // Add pickup/dropoff locations
    public required Location PickupLocation { get; set; }
    public required Location DropoffLocation { get; set; }

    // Add seat count
    public required int NumberOfSeats { get; set; }
}