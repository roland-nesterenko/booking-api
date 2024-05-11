using Booking.Infrastructure.Dwelling;

namespace Booking.Infrastructure.Database.Abstractions;

public interface IDwellingRepository : IRepository<long, DwellingEntity>
{
    
}