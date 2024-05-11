using Booking.Core.Dwelling;
using ErrorOr;

namespace Booking.Core.Booking;

public static class BookingErrors
{
    public static ErrorOr<bool> AccessDenied =>
        Error.Forbidden("Booking.AccessDenied", "Access denied");
    
    public static ErrorOr<BookingDto> InvalidBookingDate =>
        Error.Validation("Booking.InvalidBooking", "Invalid booking date");
    
    public static ErrorOr<BookingDto> AlreadyBooked =>
        Error.Validation("Booking.AlreadyBooked", "This period has already booked");
    
    public static ErrorOr<BookingDto> NotFound(long id) =>
        Error.NotFound("Booking.NotFound", $"Booking with ID ( {id} ) not found");
    
    public static ErrorOr<BookingDto> Duplicate(long tenantId, long dwellingId) =>
        Error.Conflict("Booking.Duplicate", $"Booking with Tenant ID ( {tenantId} ) and Dwelling ID ( {dwellingId} ) already exists");
}