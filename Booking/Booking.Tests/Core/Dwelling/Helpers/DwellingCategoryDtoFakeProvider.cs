using Bogus;
using Booking.Core.Dwelling;
using Booking.Tests.Helpers;

namespace Booking.Tests.Core.Dwelling.Helpers;

public class DwellingCategoryDtoFakeProvider() : ObjectFakeProviderBase<CreateDwellingDto>(5)
{
    protected override Func<Faker<CreateDwellingDto>> DefaultFactory =>
        () => new Faker<CreateDwellingDto>()
            .RuleFor(x => x.Name, f => f.Random.String())
            .RuleFor(x => x.Description, f => f.Random.String())
            .RuleFor(x => x.OwnerId, f => f.IndexFaker);
}