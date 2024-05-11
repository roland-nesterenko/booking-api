namespace Booking.Core.Auth;

public class RegisterDto
{
    public required string Name { get; init; }

    public required string Password { get; init; }

    public required string Email { get; init; }
}