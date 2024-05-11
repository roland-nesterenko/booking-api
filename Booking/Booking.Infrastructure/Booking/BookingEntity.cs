using Booking.Infrastructure.Database;
using Booking.Infrastructure.Dwelling;
using Booking.Infrastructure.User;

namespace Booking.Infrastructure.Booking;

public class BookingEntity : BaseEntity<long>
{
    public UserEntity? Tenant { get; set; }
    public required long TenantId { get; set; }
    public DwellingEntity? Dwelling { get; set; }
    public required long DwellingId { get; set; }
    
    public required DateOnly StartBookingDate { get; set; }
    
    public required DateOnly EndBookingDate { get; set; }
}