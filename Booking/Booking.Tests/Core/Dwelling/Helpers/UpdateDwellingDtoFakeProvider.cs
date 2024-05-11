using Bogus;
using Booking.Core.Dwelling;
using Booking.Tests.Helpers;

namespace Booking.Tests.Core.Dwelling.Helpers;

public class UpdateDwellingDtoFakeProvider() : ObjectFakeProviderBase<UpdateDwellingDto>(5)
{
    protected override Func<Faker<UpdateDwellingDto>> DefaultFactory =>
        () => new Faker<UpdateDwellingDto>()
            .RuleFor(x => x.Id, f => f.IndexFaker)
            .RuleFor(x => x.Name, f => f.Random.String())
            .RuleFor(x => x.Description, f => f.Random.String());
}