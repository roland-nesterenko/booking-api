using Booking.Infrastructure.Auth;
using Booking.Infrastructure.Database.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Database.Repositories;

public class RefreshTokenRepository(BookingDbContext dbContext)
    : RepositoryBase<long, RefreshTokenEntity, BookingDbContext>(dbContext), IRefreshTokenRepository
{
    public async Task<RefreshTokenEntity?> GetActiveByUserId(long userId)
    {
        return await dbContext.RefreshTokens
            .FirstOrDefaultAsync(x => x.UserId == userId && x.ExpiryDate >= DateTime.UtcNow);
    }
}