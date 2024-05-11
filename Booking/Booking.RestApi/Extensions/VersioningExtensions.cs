using Asp.Versioning;

namespace Booking.RestApi.Extensions;

public static class VersioningExtensions
{
    public static IServiceCollection AddBookingAppApiVersioning(
        this IServiceCollection services)
    {
        services.AddApiVersioning(
                options =>
                {
                    options.ReportApiVersions = true;

                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(0);
                })
            .AddApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";

                    options.SubstituteApiVersionInUrl = true;
                })
            .EnableApiVersionBinding();

        return services;
    }
}