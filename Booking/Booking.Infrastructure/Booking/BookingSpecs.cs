using System.Linq.Expressions;
using Booking.Infrastructure.Database.Specifications;

namespace Booking.Infrastructure.Booking;

public class BookingSpecs
{
    public static ByUserIdSpecification ById(long entityId) => new(entityId);

    public static ByTenantIdSpecification ByTenantId(long tenantId) => new(tenantId);

    public static ByDwellingIdSpecification ByDwellingId(long dwellingId) => new(dwellingId);

    public static ByRangeSpecification ByDateInRange(DateOnly startDate) => new(startDate);
}

public class ByTenantIdSpecification(long tenantId) : Specification<BookingEntity>
{
    public override Expression<Func<BookingEntity, bool>> ToExpression()
        => x => x.TenantId.Equals(tenantId);
}

public class ByDwellingIdSpecification(long dwellingId) : Specification<BookingEntity>
{
    public override Expression<Func<BookingEntity, bool>> ToExpression()
        => x => x.DwellingId.Equals(dwellingId);
}

public class ByUserIdSpecification(long entityId) : Specification<BookingEntity>
{
    public override Expression<Func<BookingEntity, bool>> ToExpression()
        => x => x.Id.Equals(entityId);
}

public class ByRangeSpecification(DateOnly dateOnly) : Specification<BookingEntity>
{
    public override Expression<Func<BookingEntity, bool>> ToExpression()
        => x => x.StartBookingDate <= dateOnly && x.EndBookingDate >= dateOnly;
}