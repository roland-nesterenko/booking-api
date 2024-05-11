using System.ComponentModel.DataAnnotations;

namespace Booking.RestApi.Auth.Models;

public class SignUpModel
{
    [Required] public required string Name { get; init; }

    [Required] public required string Password { get; init; }

    [Required] [EmailAddress] public required string Email { get; init; }
}