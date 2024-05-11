using Booking.Infrastructure.Auth;

namespace Booking.Infrastructure.Database.Abstractions;

public interface IRefreshTokenRepository : IRepository<long, RefreshTokenEntity>
{
    Task<RefreshTokenEntity?> GetActiveByUserId(long userId);
}