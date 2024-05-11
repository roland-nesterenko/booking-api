using Asp.Versioning;

namespace Booking.RestApi.Extensions;

public static class VersioningExtensions
{
    // Метод розширення для додавання версіювання API до сервісів (використовуюи Versioning pattern)
    public static IServiceCollection AddBookingAppApiVersioning(
        this IServiceCollection services)
    {
        // Додаємо версіювання API
        services.AddApiVersioning(
                options =>
                {
                    // Включаємо звіт про версії API
                    options.ReportApiVersions = true;

                    // Якщо версія не вказана, використовуємо версію за замовчуванням
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    
                    // Встановлюємо версію за замовчуванням
                    options.DefaultApiVersion = new ApiVersion(0);
                })
            // Додаємо API Explorer
            .AddApiExplorer(
                options =>
                {
                    // Встановлюємо формат назви групи
                    options.GroupNameFormat = "'v'VVV";

                    // Замінюємо версію API в URL
                    options.SubstituteApiVersionInUrl = true;
                })
            // Включаємо прив'язку версії API
            .EnableApiVersionBinding();

        return services;
    }
}