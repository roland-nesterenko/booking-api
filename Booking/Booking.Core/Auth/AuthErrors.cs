using ErrorOr;

namespace Booking.Core.Auth;

public static class AuthErrors
{
    public static ErrorOr<TokensDto> ValidationFailed =>
        Error.Validation("Auth.ValidationFailed", "Auth validation failed");
    
    public static ErrorOr<UserAuthDto> LoginFailed =>
        Error.Validation("Auth.ValidationFailed", "Auth login failed");
    
    public static ErrorOr<UserDto> SigUpFailed(string errorsFromManager) =>
        Error.Failure("Auth.ValidationFailed", $"Auth login failed with: {errorsFromManager}");
    
    public static ErrorOr<UserDto> Duplicate(string email) =>
        Error.Conflict("Auth.Duplicate", $"User with Email ( {email} ) already exists");
}