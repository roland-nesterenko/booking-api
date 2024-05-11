namespace Booking.Core.Auth;

public record LoginDto
{
    public required string Login { get; init; }

    public required string Password { get; init; }
}