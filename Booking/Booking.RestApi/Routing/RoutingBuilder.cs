using Booking.RestApi.Auth;
using Booking.RestApi.Booking;
using Booking.RestApi.Dwelling;

namespace Booking.RestApi.Routing;

public static class RoutingBuilder
{
    public static void MapRouting(this IEndpointRouteBuilder routeBuilder)
    {
        var app = routeBuilder.NewVersionedApi();

        var serviceHealthV1 = app.MapGroup("/api/health/")
            .WithTags("health")
            .HasApiVersion(1);

        serviceHealthV1
            .MapHealthChecks("health");
        serviceHealthV1
            .MapGet("status", () => Results.Ok("Ok"));
        
        app.AddBookingEndpoints();
        app.AddDwellingEndpoints();
        app.AddAuthEndpoints();
    }
}