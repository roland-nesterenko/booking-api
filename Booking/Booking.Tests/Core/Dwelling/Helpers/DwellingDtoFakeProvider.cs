using Bogus;
using Booking.Core.Dwelling;
using Booking.Tests.Helpers;

namespace Booking.Tests.Core.Dwelling.Helpers;

public class DwellingDtoFakeProvider() : ObjectFakeProviderBase<DwellingDto>(5)
{
    protected override Func<Faker<DwellingDto>> DefaultFactory =>
        () => new Faker<DwellingDto>()
            .RuleFor(x => x.Id, f => f.IndexFaker)
            .RuleFor(x => x.Name, f => f.Random.String())
            .RuleFor(x => x.Description, f => f.Random.String())
            .RuleFor(x => x.OwnerId, f => f.IndexFaker);
}