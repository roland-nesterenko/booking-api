using Asp.Versioning.Builder;
using Booking.Core.Dwelling;
using Booking.RestApi.Dwelling.Models;
using Booking.RestApi.Extensions;

namespace Booking.RestApi.Dwelling;

public static class DwellingModule
{
    internal static void AddDwellingEndpoints(this IVersionedEndpointRouteBuilder app)
    {
        var builderV1 = app.MapGroup("/api/dwellings/")
            .WithTags("Dwelling")
            .HasApiVersion(1)
            .RequireAuthorization();
        
        builderV1.MapPost("", async (
            HttpContext httpContext,
            CreateDwellingViewModel req,
            IDwellingService service,
            CancellationToken _) =>
        {
            var dto = new CreateDwellingDto()
            {
                Name = req.Name,
                Description = req.Description,
                OwnerId = httpContext.User.GetUserId()
            };

            var result = await service.Create(dto);
            return result.Match(Results.Ok, e => e.Problem(context: httpContext));
        });

        builderV1.MapGet("", async (
            HttpContext httpContext,
            IDwellingService service,
            CancellationToken _) =>
        {
            var result = await service.GetAll();
            return result.Match(Results.Ok, e => e.Problem(context: httpContext));
        });

        builderV1.MapGet("{id:long}", async (
            long id,
            HttpContext httpContext,
            IDwellingService service,
            CancellationToken _) =>
        {
            var result = await service.Get(id);
            return result.Match(Results.Ok, e => e.Problem(context: httpContext));
        });

        builderV1.MapPut("{id:long}", async (
            long id,
            UpdateDwellingViewModel req,
            HttpContext httpContext,
            IDwellingService service,
            CancellationToken _) =>
        {
            var dto = new UpdateDwellingDto()
            {
                Id = id,
                Name = req.Name,
                Description = req.Description,
            };

            var result = await service.Update(dto, httpContext.User.GetUserId());
            return result.Match(Results.Ok, e => e.Problem(context: httpContext));
        });

        builderV1.MapDelete("{id:long}", async (
            long id,
            HttpContext httpContext,
            IDwellingService service,
            CancellationToken _) =>
        {
            var result = await service.Delete(id, httpContext.User.GetUserId());
            return result.Match(Results.Ok, e => e.Problem(context: httpContext));
        });
    }
}