using Booking.RestApi.Auth;
using Booking.RestApi.Booking;
using Booking.RestApi.Dwelling;

namespace Booking.RestApi.Routing;

public static class RoutingBuilder
{
    // Метод розширення для налаштування маршрутизації
    public static void MapRouting(this IEndpointRouteBuilder routeBuilder)
    {
        // Створюємо новий версіонований API
        var app = routeBuilder.NewVersionedApi();

        // Налаштовуємо групу маршрутів для перевірки стану сервісу
        var serviceHealthV1 = app.MapGroup("/api/health/")
            .WithTags("health")
            .HasApiVersion(1);

        // Додаємо маршрут для перевірки стану сервісу
        serviceHealthV1
            .MapHealthChecks("health");
        serviceHealthV1
            .MapGet("status", () => Results.Ok("Ok"));
        
        // Додаємо маршрути для резервування житла, житла та авторизації
        app.AddBookingEndpoints();
        app.AddDwellingEndpoints();
        app.AddAuthEndpoints();
    }
}