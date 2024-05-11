using System.Linq.Expressions;
using Booking.Infrastructure.Database.Specifications;

namespace Booking.Infrastructure.Dwelling;

public class DwellingSpecs
{
    public static ByIdSpecification ById(long id) => new(id);
    public static ByOwnerIdSpecification ByOwnerId(long id) => new(id);
    public static ByNameSpecification ByName(string name) => new(name);
}

public class ByNameSpecification(string name) : Specification<DwellingEntity>
{
    public override Expression<Func<DwellingEntity, bool>> ToExpression()
        => x => x.Name.Equals(name);
}

public class ByIdSpecification(long id) : Specification<DwellingEntity>
{
    public override Expression<Func<DwellingEntity, bool>> ToExpression()
        => x => x.Id.Equals(id);
}

public class ByOwnerIdSpecification(long id) : Specification<DwellingEntity>
{
    public override Expression<Func<DwellingEntity, bool>> ToExpression()
        => x => x.OwnerId.Equals(id);
}