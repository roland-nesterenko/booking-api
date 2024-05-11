using System.Text;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Booking.RestApi.Swagger.Options;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Booking.RestApi.Swagger;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    // Провайдер опису версій API
    private readonly IApiVersionDescriptionProvider _provider;

    // Опції контактної інформації Swagger
    private readonly SwaggerContactsInfoOptions _contactInfo;

    public ConfigureSwaggerOptions(
        IApiVersionDescriptionProvider provider,
        IOptions<SwaggerContactsInfoOptions> contactOptions)
    {
        _provider = provider;
        // Перевірка на null значення опцій контактної інформації
        ArgumentNullException.ThrowIfNull(contactOptions.Value);
        _contactInfo = contactOptions.Value;
    }

    // Метод конфігурації SwaggerGenOptions
    public void Configure(SwaggerGenOptions options)
    {
        // Для кожного опису версії API додаємо документацію Swagger
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }
    }

    // Створення інформації для версії API
    private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        // Ініціалізація тексту опису
        var text = new StringBuilder($"Test task v{description.ApiVersion}.");

        // Створення інформації API
        var info = new OpenApiInfo()
        {
            Title = "Test task",
            Version = description.ApiVersion.ToString(),
            Contact = new OpenApiContact
                { Name = "Test task - Kirill Nesterenko", Url = new Uri(_contactInfo.ContactUri!) },
            License = new OpenApiLicense { Name = "MIT", Url = new Uri(_contactInfo.LicenseUri!) }
        };

        // Якщо версія API застаріла, додаємо відповідний текст
        if (description.IsDeprecated)
        {
            text.Append(" This API version has been deprecated.");
        }

        // Якщо є політика закату, додаємо відповідну інформацію
        if (description.SunsetPolicy is { } policy)
        {
            GetApiVersionSunsetInfo(policy, text);
        }

        // Встановлюємо опис API
        info.Description = text.ToString();

        return info;
    }

    // Отримання інформації про закат версії API
    private static void GetApiVersionSunsetInfo(SunsetPolicy policy, StringBuilder text)
    {
        // Якщо є дата закату, додаємо її в текст
        if (policy.Date is { } when)
        {
            text.Append("The API will be sunset on ")
                .Append(when.Date.ToShortDateString())
                .Append('.');
        }

        if (!policy.HasLinks)
        {
            return;
        }

        text.AppendLine();

        foreach (var link in policy.Links)
        {
            GetLinkInfo(text, link);
        }
    }

    // Отримання інформації про посилання для Swagger
    private static void GetLinkInfo(StringBuilder text, LinkHeaderValue link)
    {
        if (link.Type != "text/html")
        {
            return;
        }

        text.AppendLine();

        if (link.Title.HasValue)
        {
            text.Append(link.Title.Value).Append(": ");
        }

        text.Append(link.LinkTarget.OriginalString);
    }
}