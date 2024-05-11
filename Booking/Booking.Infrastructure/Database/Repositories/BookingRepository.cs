using Booking.Infrastructure.Booking;
using Booking.Infrastructure.Database.Abstractions;

namespace Booking.Infrastructure.Database.Repositories;

public class BookingRepository(BookingDbContext dbContext)
    : RepositoryBase<long, BookingEntity, BookingDbContext>(dbContext), IBookingRepository
{
}