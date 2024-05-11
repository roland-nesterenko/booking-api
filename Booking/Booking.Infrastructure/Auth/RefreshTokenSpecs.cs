using System.Linq.Expressions;
using Booking.Infrastructure.Database.Specifications;

namespace Booking.Infrastructure.Auth;

public class RefreshTokenSpecs
{
    public static ByUserIdSpecification ByUserId(long id) => new(id);
}

public class ByUserIdSpecification(long id) : Specification<RefreshTokenEntity>
{
    public override Expression<Func<RefreshTokenEntity, bool>> ToExpression()
        => x => x.UserId.Equals(id);
}