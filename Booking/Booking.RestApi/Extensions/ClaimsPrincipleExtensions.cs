using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace Booking.RestApi.Extensions;

public static class ClaimsPrincipleExtensions
{
    public static string? GetUsername(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.Name)?.Value;
    }

    public static string? GetEmail(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.Email)?.Value;
    }

    public static long GetUserId(this ClaimsPrincipal user)
    {
        return long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
}