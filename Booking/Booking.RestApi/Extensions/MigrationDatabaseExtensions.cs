using Booking.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Booking.RestApi.Extensions;

public static class MigrationDatabaseExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();

        using var dbContext = serviceScope.ServiceProvider.GetRequiredService<BookingDbContext>();
        
        dbContext.Database.Migrate();
    }
}