using Booking.Infrastructure.Database.Abstractions;
using Booking.Infrastructure.Dwelling;

namespace Booking.Infrastructure.Database.Repositories;

public class DwellingRepository(BookingDbContext dbContext)
    : RepositoryBase<long, DwellingEntity, BookingDbContext>(dbContext), IDwellingRepository
{
    
}