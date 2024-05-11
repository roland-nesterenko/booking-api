using System.ComponentModel.DataAnnotations;
using Booking.Infrastructure.Database;
using Booking.Infrastructure.User;

namespace Booking.Infrastructure.Auth;

public class RefreshTokenEntity : BaseEntity<long>
{
    [Required]
    public required string TokenHash { get; set; }
    
    [Required]
    public required DateTime CreatedDate { get; set; }
    
    [Required]
    public required DateTime ExpiryDate { get; set; }
    
    [Required]
    public required long UserId { get; set; }
    public UserEntity? UserEntity { get; set; }
}