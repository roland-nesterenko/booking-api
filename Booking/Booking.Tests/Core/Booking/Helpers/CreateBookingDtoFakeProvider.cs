using Bogus;
using Booking.Core.Booking;
using Booking.Tests.Helpers;

namespace Booking.Tests.Core.Booking.Helpers;

public class CreateBookingDtoFakeProvider() : ObjectFakeProviderBase<CreateBookingDto>(5)
{
    protected override Func<Faker<CreateBookingDto>> DefaultFactory =>
        () => new Faker<CreateBookingDto>()
            .RuleFor(x => x.DwellingId, f => f.IndexFaker)
            .RuleFor(x => x.TenantId, f => f.IndexFaker)
            .RuleFor(x => x.StartBookingDate, f => f.Date.FutureDateOnly())
            .RuleFor(x => x.EndBookingDate, f => f.Date.FutureDateOnly());
}