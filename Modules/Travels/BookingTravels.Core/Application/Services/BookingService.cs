using BookingTravels.Core.Application.Contracts;
using BookingTravels.Core.Application.Contracts.Bookings;
using BookingTravels.Core.Application.Contracts.Travels;
using BookingTravels.Core.Domain.Entities;
using BookingTravels.Core.Domain.ValueObjects;
using CarPool.Shared.Events.Common.Interfaces;
using CarPool.Shared.Events.Events;
using Enum = CarPool.Shared.Events.Enums.Enum;

namespace BookingTravels.Core.Application.Services;

public class BookingService: IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ITravelRepository _travelRepository;
        private readonly IEventPublisher _eventBus;

        public BookingService(IBookingRepository bookingRepository, ITravelRepository travelRepository, IEventPublisher eventBus)
        {
            _bookingRepository = bookingRepository;
            _travelRepository = travelRepository;
            _eventBus = eventBus;
        }

        public async Task<Booking> CreateBookingAsync(Guid travelId, Guid clientId, int seats)
        {
            if (seats <= 0)
                throw new ArgumentException("Seats booked must be greater than zero.");
            
            var travel = await _travelRepository.GetByIdAsync(travelId);
            if (travel == null || travel.Status != Enum.TravelStatus.Open)
                throw new Exception("Travel not available.");

            if (travel.AvailableSeats < seats)
                throw new Exception("Not enough available seats.");
            
            var totalPrice = Money.Create(seats * travel.PricePerSeat.Amount, travel.PricePerSeat.Currency);
            
            await _travelRepository.ReserveSeatsAsync(travelId, seats);
            
            var booking = new Booking(travelId, clientId, seats, totalPrice);
            await _bookingRepository.AddAsync(booking);

            var bookingCreatedEvent = new BookingCreatedEvent(
                booking.Id,
                travelId,
                clientId,
                seats,
                totalPrice.Amount,
                booking.BookingDate
            );
            await _eventBus.PublishAsync(bookingCreatedEvent);

            return booking;
        }

        public async Task CancelBookingAsync(Guid bookingId, Guid clientId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null)
                throw new Exception("Booking not found.");

            if (booking.ClientId != clientId)
                throw new Exception("Unauthorized to cancel this booking.");

            if (booking.Status == Enum.BookingStatus.Cancelled)
                throw new Exception("Booking is already cancelled.");
            
            booking.CancelBooking();
            await _bookingRepository.UpdateAsync(booking);
            
            await _travelService.ReleaseSeatsAsync(booking.TravelId, booking.SeatsBooked);
            
            var bookingCancelledEvent = new BookingCancelledEvent(
                booking.Id,
                booking.TravelId,
                clientId,
                booking.SeatsBooked,
                booking.CancellationDate.Value
            );
            await _eventBus.PublishAsync(bookingCancelledEvent);
        }

        public async Task<Booking> GetBookingByIdAsync(Guid bookingId)
        {
            return await _bookingRepository.GetByIdAsync(bookingId);
        }

        public async Task<IEnumerable<Booking>> GetBookingsByClientIdAsync(Guid clientId)
        {
            return await _bookingRepository.GetBookingsByClientIdAsync(clientId);
        }
    }