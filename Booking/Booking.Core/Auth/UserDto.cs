namespace Booking.Core.Auth;

public record UserDto
{
    public required string Name { get; set; }
    public required string Email { get; set; }
}