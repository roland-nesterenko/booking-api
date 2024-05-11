using System.ComponentModel.DataAnnotations;

namespace Booking.Core.Auth;

public record TokensDto
{
    public required string AccessToken { get; set; }
    
    public required string RefreshToken { get; set; }
}