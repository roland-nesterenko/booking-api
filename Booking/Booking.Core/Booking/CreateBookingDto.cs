namespace Booking.Core.Booking;

public record CreateBookingDto
{
    public required long TenantId { get; init; }

    public required long DwellingId { get; init; }

    public required DateOnly StartBookingDate { get; set; }
    
    public required DateOnly EndBookingDate { get; set; }
}