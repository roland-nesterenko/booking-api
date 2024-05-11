using ErrorOr;

namespace Booking.Core.Auth;

public static class AuthErrors
{
    // Помилка валідації, що повертається, коли не вдається валідація авторизації
    public static ErrorOr<TokensDto> ValidationFailed =>
        Error.Validation("Auth.ValidationFailed", "Auth validation failed");
    
    // Помилка валідації, що повертається, коли не вдається вхід в систему
    public static ErrorOr<UserAuthDto> LoginFailed =>
        Error.Validation("Auth.ValidationFailed", "Auth login failed");
    
    // Помилка, що повертається, коли реєстрація не вдалася, виводячи усі помилки з UserManager
    public static ErrorOr<UserDto> SigUpFailed(string errorsFromManager) =>
        Error.Failure("Auth.ValidationFailed", $"Auth login failed with: {errorsFromManager}");
    
    // Помилка конфлікту, що повертається, коли користувач з такою електронною поштою вже існує
    public static ErrorOr<UserDto> Duplicate(string email) =>
        Error.Conflict("Auth.Duplicate", $"User with Email ( {email} ) already exists");
}