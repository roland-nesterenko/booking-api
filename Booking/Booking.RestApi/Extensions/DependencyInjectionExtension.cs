using Booking.Infrastructure.Database;
using Booking.RestApi.Swagger.Options;
using Microsoft.EntityFrameworkCore;

namespace Booking.RestApi.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SwaggerContactsInfoOptions>(
            configuration.GetSection(SwaggerContactsInfoOptions.SectionName));

        return services;
    }
    
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BookingDbContext>(opt =>
        {
            opt.UseSqlServer(configuration["Database:DefaultConnection"]);
        });

        return services;
    }
    
    public static IServiceCollection SetupCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
        });

        return services;
    }
}