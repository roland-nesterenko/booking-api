using Asp.Versioning.Builder;
using Booking.Core.Booking;
using Booking.RestApi.Booking.Models;
using Booking.RestApi.Extensions;

namespace Booking.RestApi.Booking;

public static class BookingModule
{
    internal static void AddBookingEndpoints(this IVersionedEndpointRouteBuilder app)
    {
        var profileV1 = app.MapGroup("/api/bookings/")
            .WithTags("Booking")
            .HasApiVersion(1)
            .RequireAuthorization();
        
        profileV1.MapGet("", async (
            HttpContext httpContext,
            IBookingService service,
            CancellationToken _) =>
        {
            var result = await service.GetAll();
            return result.Match(Results.Ok, e => e.Problem(context: httpContext));
        });
        
        profileV1.MapGet("{id:int}", async (
            int id,
            HttpContext httpContext,
            IBookingService service,
            CancellationToken _) =>
        {
            var result = await service.Get(id);
        
            return result.Match(Results.Ok, e => e.Problem(context: httpContext));
        });
        
        profileV1.MapPost("", async (
            HttpContext httpContext,
            CreateBookingViewModel req,
            IBookingService service,
            CancellationToken _) =>
        {
            var createCategoryDto = new CreateBookingDto()
            {
                DwellingId = req.DwellingId,
                TenantId = httpContext.User.GetUserId(),
                StartBookingDate = req.StartBookingDate,
                EndBookingDate = req.EndBookingDate
            };
        
            var result = await service.Create(createCategoryDto);
            return result.Match(Results.Ok, e => e.Problem(context: httpContext));
        });
        
        profileV1.MapDelete("{id:int}", async (
            long id,
            HttpContext httpContext,
            IBookingService service,
            CancellationToken _) =>
        {
            var result = await service.Delete(id, httpContext.User.GetUserId());
            return result.Match(Results.Ok, e => e.Problem(context: httpContext));
        });
    }
}