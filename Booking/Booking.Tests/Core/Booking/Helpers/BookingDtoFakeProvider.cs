using Bogus;
using Booking.Core.Booking;
using Booking.Tests.Helpers;

namespace Booking.Tests.Core.Booking.Helpers;

public class BookingDtoFakeProvider() : ObjectFakeProviderBase<BookingDto>(5)
{
    protected override Func<Faker<BookingDto>> DefaultFactory =>
        () => new Faker<BookingDto>()
            .RuleFor(x => x.Id, f => f.IndexFaker)
            .RuleFor(x => x.DwellingId, f => f.IndexFaker)
            .RuleFor(x => x.TenantId, f => f.IndexFaker);
}