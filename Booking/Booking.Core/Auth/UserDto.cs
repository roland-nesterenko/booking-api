namespace Booking.Core.Auth;

public record UserDto
{
    public required string Name { get; init; }
    public required string Email { get; init; }
}