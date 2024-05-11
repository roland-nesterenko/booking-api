namespace Booking.Core.Booking;

public record BookingDto
{
    public required long Id { get; init; }
    
    public required long TenantId { get; init; }

    public required long DwellingId { get; init; }
    
    public required DateOnly StartBookingDate { get; init; }
    
    public required DateOnly EndBookingDate { get; init; }
}