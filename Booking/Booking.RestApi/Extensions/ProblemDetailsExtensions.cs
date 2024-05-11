using ErrorOr;

namespace Booking.RestApi.Extensions;

public static class ProblemDetailsExtensions
{
    // Константа для ключа помилок
    private const string Errors = "Errors";
    
    // Метод розширення для обробки помилок
    public static IResult Problem(this List<Error> errors, HttpContext context)
    {
        // Додаємо помилки до контексту
        context.Items.Add(Errors, errors);
        
        // Якщо всі помилки є помилками валідації, повертаємо відповідний результат
        if (errors.All(error => error.Type is ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        // Беремо першу помилку зі списку
        var firstError = errors[0];

        // Визначаємо статус код відповідно до типу помилки
        var statusCode = firstError.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        };

        // Повертаємо результат з відповідним статус кодом та описом помилки
        return Results.Problem(statusCode: statusCode, title: firstError.Description);
    }

    // Приватний метод для обробки помилок валідації
    private static IResult ValidationProblem(List<Error> errors)
    {
        // Створюємо словник з помилками для моделі стану
        var modelState = errors.ToDictionary<Error, string, string[]>(
            error => error.Code, error => [error.Description]);

        // Повертаємо результат з помилками валідації
        return Results.ValidationProblem(modelState);
    }
}