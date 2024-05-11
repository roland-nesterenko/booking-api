namespace Booking.Core.Auth;

public record UserAuthDto
{
    public required UserDto User { get; set; }
    public required TokensDto Tokens { get; set; } 
}