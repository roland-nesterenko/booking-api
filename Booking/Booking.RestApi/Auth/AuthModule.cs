using Asp.Versioning.Builder;
using Booking.Core.Auth;
using Booking.RestApi.Auth.Models;
using Booking.RestApi.Extensions;

namespace Booking.RestApi.Auth;

public static class AuthModule
{
    internal static void AddAuthEndpoints(this IVersionedEndpointRouteBuilder app)
    {
        var builderV1 = app.MapGroup("/api/auth/")
            .WithTags("Auth")
            .HasApiVersion(1);

        builderV1.MapPost("sign-up", async (
            HttpContext httpContext,
            SignUpModel req,
            IAuthService service,
            CancellationToken _) =>
        {
            var dto = new RegisterDto()
                { Email = req.Email, Name = req.Name, Password = req.Password };

            var result = await service.SignUp(dto);
            return result.Match(Results.Ok, e => e.Problem(context: httpContext));
        });
        
        builderV1.MapPost("sign-in", async (
            HttpContext httpContext,
            LoginModel req,
            IAuthService service,
            CancellationToken _) =>
        {
            var dto = new LoginDto()
                { Login = req.Email, Password = req.Password };

            var result = await service.SignIn(dto);
            return result.Match(Results.Ok, e => e.Problem(context: httpContext));
        });
    }
}