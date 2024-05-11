using System.ComponentModel.DataAnnotations;
using Booking.Infrastructure.Dwelling;
using Microsoft.AspNetCore.Identity;

namespace Booking.Infrastructure.User;

public class UserEntity : IdentityUser<long>
{
    [MaxLength(128)]
    public required string Name { get; set; }
    
    public List<DwellingEntity>? Dwellings { get; set; } = [];
}