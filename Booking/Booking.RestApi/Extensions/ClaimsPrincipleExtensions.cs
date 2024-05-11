using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace Booking.RestApi.Extensions;

public static class ClaimsPrincipleExtensions
{
    // Метод для отримання імені користувача з ClaimsPrincipal
    public static string? GetUsername(this ClaimsPrincipal user)
    {
        // Повертає значення першого знайденого Claim з типом Name, якщо він існує
        return user.FindFirst(ClaimTypes.Name)?.Value;
    }

    // Метод для отримання електронної пошти користувача з ClaimsPrincipal
    public static string? GetEmail(this ClaimsPrincipal user)
    {
        // Повертає значення першого знайденого Claim з типом Email, якщо він існує
        return user.FindFirst(ClaimTypes.Email)?.Value;
    }

    // Метод для отримання ID користувача з ClaimsPrincipal
    public static long GetUserId(this ClaimsPrincipal user)
    {
        // Повертає значення першого знайденого Claim з типом NameIdentifier, якщо він існує
        // Значення парситься в long
        return long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
    }
}