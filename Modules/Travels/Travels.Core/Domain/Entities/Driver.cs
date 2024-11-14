namespace Travels.Core.Domain.Entities;

public class Driver
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string PhoneNumber { get; private set; }
    public Car Car { get; private set; }
    public double Rating { get; private set; }
    public int CompletedTrips { get; private set; }

    private Driver() { } // For EF Core

    public Driver(
        Guid id,
        string email,
        string firstName,
        string lastName,
        string phoneNumber,
        Car car)
    {
        Id = id;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Car = car;
        Rating = 5.0; // Default rating
        CompletedTrips = 0;
    }

    public void UpdateCar(Car car)
    {
        Car.Update(car.Brand, car.Color, car.Number);
    }
}