using System.Text;
using Booking.Infrastructure.Database;
using Booking.Infrastructure.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Booking.RestApi.Extensions;

public static class AuthExtensions
{
    // Метод для додавання ідентифікації користувача для нашого застосунку
    internal static void AddBookingIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        // Додаємо стандартне Identity (ASP.NET Identity) з використанням UserEntity та UserRoleEntity
        services.AddIdentity<UserEntity, UserRoleEntity>()
            .AddEntityFrameworkStores<BookingDbContext>() // Використовуємо BookingDbContext для зберігання даних Identity
            .AddDefaultTokenProviders(); // Додаємо стандартні провайдери токенів

        // Додаємо розширення авторизації
        services
            .AddBookingAuthorization();

        // Додаємо розширенн аутентифікацію
        services
            .AddBookingAuthentication(configuration);
    }

    // Метод розширення для додавання авторизації
    private static void AddBookingAuthorization(
        this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            var policy = new AuthorizationPolicyBuilder("Bearer").RequireAuthenticatedUser().Build();

            options.DefaultPolicy = policy;
        });
    }

    // Метод розширення для додавання аутентифікації
    private static void AddBookingAuthentication(
        this IServiceCollection services, IConfiguration configuration)
    {
        // Використовуємо схему аутентифікації JwtBearer
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });
    }
}