using Booking.Infrastructure.Database.Abstractions;
using Booking.Infrastructure.Database.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Infrastructure;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBookingAppInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IDwellingRepository, DwellingRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        
        return services;
    }
}