using Booking.Core.Auth;
using Booking.Core.Booking;
using Booking.Core.Dwelling;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Core;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBookingAppCore(this IServiceCollection services)
    {
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IDwellingService, DwellingService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
        services.AddScoped<ITokenService, TokenService>();
        
        return services;
    }
}