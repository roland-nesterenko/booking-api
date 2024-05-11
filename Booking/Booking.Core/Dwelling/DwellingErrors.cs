using ErrorOr;

namespace Booking.Core.Dwelling;

public static class DwellingErrors
{
    public static ErrorOr<bool> AccessDenied =>
        Error.Forbidden("Dwelling.AccessDenied", "Access denied");
    
    public static ErrorOr<DwellingDto> NotFound(long id) =>
        Error.NotFound("Dwelling.NotFound", $"Dwelling with ID( {id} ) not found");
    
    public static ErrorOr<bool> NotFoundForBooking(long id) =>
        Error.NotFound("Dwelling.NotFound", $"Dwelling with ID( {id} ) not found");

    public static ErrorOr<DwellingDto> Duplicate(string name) =>
        Error.Conflict("Dwelling.Duplicate", $"Dwelling with Name ( {name} ) already exists");
}