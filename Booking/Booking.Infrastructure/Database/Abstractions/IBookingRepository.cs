using Booking.Infrastructure.Booking;

namespace Booking.Infrastructure.Database.Abstractions;

public interface IBookingRepository : IRepository<long, BookingEntity>
{
    
}