namespace Booking.Core.Auth;

public class LoginDto
{
    public required string Login { get; init; }

    public required string Password { get; init; }
}