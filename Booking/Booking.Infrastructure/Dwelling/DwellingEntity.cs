using System.ComponentModel.DataAnnotations;
using Booking.Infrastructure.Database;
using Booking.Infrastructure.User;

namespace Booking.Infrastructure.Dwelling;

public class DwellingEntity : BaseEntity<long>
{
    [MaxLength(128)]
    public required string Name { get; set; }
    
    [MaxLength(256)]
    public required string Description { get; set; }
    
    public required long OwnerId { get; set; }
    public UserEntity? Owner { get; set; }
}