using CarPool.Shared.Events.Common.Entities;
using CarPool.Shared.Events.Exceptions;
using Travels.Core.Domain.ValueObjects;
using Enum = CarPool.Shared.Events.Enums.Enum;

namespace Travels.Core.Domain.Entities;

public class Travel : BaseEntity<Guid>
{
    private Travel() { } // For EF Core

    public Travel(
        Guid driverId,
        Location origin,
        Location destination,
        DateTime departureTime,
        int availableSeats,
        Money pricePerSeat)
    {
        Id = Guid.NewGuid();
        DriverId = driverId;
        Origin = origin ?? throw new ArgumentNullException(nameof(origin));
        Destination = destination ?? throw new ArgumentNullException(nameof(destination));
        DepartureTime = departureTime;
        AvailableSeats = availableSeats;
        PricePerSeat = pricePerSeat ?? throw new ArgumentNullException(nameof(pricePerSeat));
        Status = Enum.TravelStatus.Created;
        
        ValidateState();
    }

    public Guid DriverId { get; private set; }
    public Location Origin { get; private set; }
    public Location Destination { get; private set; }
    public DateTime DepartureTime { get; private set; }
    public int AvailableSeats { get; private set; }
    public Money PricePerSeat { get; private set; }
    public Enum.TravelStatus Status { get; private set; }
    
    public Driver Driver { get; private set; }

    // private readonly List<Booking> _bookings = new();
    // public IReadOnlyCollection<Booking> Bookings => _bookings.AsReadOnly();

    public void UpdateDetails(
        Location origin,
        Location destination,
        DateTime departureTime,
        int availableSeats,
        Money pricePerSeat)
    {
        if (Status != Enum.TravelStatus.Created)
            throw new DomainException("Cannot update travel after it's been published");

        Origin = origin ?? throw new ArgumentNullException(nameof(origin));
        Destination = destination ?? throw new ArgumentNullException(nameof(destination));
        DepartureTime = departureTime;
        AvailableSeats = availableSeats;
        PricePerSeat = pricePerSeat ?? throw new ArgumentNullException(nameof(pricePerSeat));

        ValidateState();
    }

    public void Publish()
    {
        if (Status != Enum.TravelStatus.Created)
            throw new DomainException("Travel is already published");

        Status = Enum.TravelStatus.Open;
    }

    public void Cancel()
    {
        if (Status != Enum.TravelStatus.Open)
            throw new DomainException("Can only cancel open travels");

        Status = Enum.TravelStatus.Cancelled;
    }

    private void ValidateState()
    {
        if (DepartureTime <= DateTime.UtcNow)
            throw new DomainException("Departure time must be in the future");

        if (AvailableSeats <= 0)
            throw new DomainException("Available seats must be greater than zero");

        if (Origin.Equals(Destination))
            throw new DomainException("Origin and destination cannot be the same");
    }
}