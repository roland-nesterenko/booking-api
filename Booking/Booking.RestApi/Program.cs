using Booking.Core;
using Booking.Infrastructure;
using Booking.RestApi.Routing;
using Booking.RestApi.Extensions;

namespace Booking.RestApi;

public static class Program
{
    internal static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddServices();
        
        var app = builder.Build();

        app.ConfigureMiddlewares();

        app.Run();
    }

    private static void AddServices(this IHostApplicationBuilder appBuilder)
    {
        var services = appBuilder.Services;

        var logging = appBuilder.Logging;
        
        services
            .AddBookingIdentity(appBuilder.Configuration);
        
        logging
            .AddBookingLogging();
        
        services
            .AddDatabase(appBuilder.Configuration);

        services
            .ConfigureOptions(appBuilder.Configuration)
            .SetupCorsPolicy();
        
        services
            .AddBookingAppSwaggerGen()
            .AddProblemDetails()
            .AddHealthChecks();

        services
            .AddBookingAppApiVersioning();
            
        
        services
            .AddBookingAppInfrastructure(appBuilder.Configuration)
            .AddBookingAppCore();
    }
    
    private static void ConfigureMiddlewares(this WebApplication appBuilder)
    {
        if (appBuilder.Environment.IsDevelopment())
        {
            appBuilder.UseDeveloperExceptionPage();
            appBuilder.ApplyMigrations();
        }

        appBuilder.UseCors("CorsPolicy");
        
        appBuilder.UseAuthentication();
        appBuilder.UseAuthorization();
        
        appBuilder.MapRouting();
        appBuilder.UseSwaggerUi();
    }
}