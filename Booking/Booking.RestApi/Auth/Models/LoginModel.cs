using System.ComponentModel.DataAnnotations;

namespace Booking.RestApi.Auth.Models;

public class LoginModel
{
    [Required] public required string Email { get; init; }

    [Required] public required string Password { get; init; }
}