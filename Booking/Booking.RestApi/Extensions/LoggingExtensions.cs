using Serilog;
using Serilog.Formatting.Json;

namespace Booking.RestApi.Extensions;

public static class LoggingExtensions
{
    // Метод для додавання налаштувань логування Serilog до ILoggingBuilder
    public static ILoggingBuilder AddBookingLogging(this ILoggingBuilder logging)
    {
        // Додаємо Serilog до ILoggingBuilder
        logging.AddSerilog(new LoggerConfiguration()
            .Enrich.FromLogContext() // Збагачуємо кожне повідомлення логу контекстом, з якого воно було відправлено
            .MinimumLevel.Warning() // Встановлюємо мінімальний рівень логування до Warning для того, щоб не було спаму з Infromation у файлі
            .WriteTo.File(new JsonFormatter(), "Logs/log-Serilog-jsonFile.log") // Додаємо запис у файл з форматуванням Json, встановлюючи шлях
            .CreateLogger()); // Створюємо логер на основі вказаної конфігурації
        
        return logging;
    }
}