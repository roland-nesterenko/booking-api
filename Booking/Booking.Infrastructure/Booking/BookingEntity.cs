using Booking.Infrastructure.Database;

namespace Booking.Infrastructure.Booking;

public class BookingEntity : BaseEntity<long>
{
    public required long TenantId { get; set; }
    public required long DwellingId { get; set; }
    
    public required DateOnly StartBookingDate { get; set; }
    
    public required DateOnly EndBookingDate { get; set; }
}