namespace Booking.Core.Auth;

public record RegisterDto
{
    public required string Name { get; init; }

    public required string Password { get; init; }

    public required string Email { get; init; }
}