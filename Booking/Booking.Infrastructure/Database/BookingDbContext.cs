using System.Reflection;
using Booking.Infrastructure.Auth;
using Booking.Infrastructure.Booking;
using Booking.Infrastructure.Dwelling;
using Booking.Infrastructure.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Booking.Infrastructure.Database;

public class BookingDbContext(DbContextOptions<BookingDbContext> options)
    : IdentityDbContext<UserEntity, UserRoleEntity, long>(options)
{
    public DbSet<UserEntity> Users { get; set; } = default!;
    public DbSet<RefreshTokenEntity> RefreshTokens { get; set; } = default!;

    public DbSet<DwellingEntity> Dwellings { get; set; } = default!;

    public DbSet<BookingEntity> Bookings { get; set; } = default!;


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        // var roles = new List<UserRoleEntity>() { new UserRoleEntity { Name = "user" } };
    }
}