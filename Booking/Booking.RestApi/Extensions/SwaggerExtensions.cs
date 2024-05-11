using Booking.RestApi.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Booking.RestApi.Extensions;

public static class SwaggerExtensions
{
    // Метод розширення для конфігурації SwaggerGen в сервісах
    public static IServiceCollection AddBookingAppSwaggerGen(
        this IServiceCollection services)
    {
        // Додаємо API Explorer, який використовується SwaggerGen для генерації документації
        services.AddEndpointsApiExplorer();
        
        // Додаємо конфігурацію SwaggerGenOptions
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        
        // Додаємо SwaggerGen з налаштуваннями безпеки
        services.AddSwaggerGen(options =>
        {
            // Додаємо визначення безпеки для JWT
            options.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                });
            
            // Додаємо вимоги до безпеки для JWT
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    []
                }
            });
        });
        
        return services;
    }
    
    // Метод розширення для використання Swagger UI
    public static IApplicationBuilder UseSwaggerUi(
        this IApplicationBuilder app)
    {
        app
            .UseSwagger()
            .UseSwaggerUI();

        return app;
    }
}