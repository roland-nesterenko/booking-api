using Serilog;
using Serilog.Formatting.Json;

namespace Booking.RestApi.Extensions;

public static class LoggingExtensions
{
    public static ILoggingBuilder AddBookingLogging(this ILoggingBuilder logging)
    {
        logging.AddSerilog(new LoggerConfiguration()
            .Enrich.FromLogContext()
            .MinimumLevel.Warning()
            .WriteTo.File(new JsonFormatter(), "Logs/log-Serilog-jsonFile.log")
            .CreateLogger());
        
        return logging;
    }
}