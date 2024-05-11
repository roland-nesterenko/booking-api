using System.Text;
using Booking.Infrastructure.User;
using Microsoft.AspNetCore.Identity;
using ErrorOr;

namespace Booking.Core.Auth;

public interface IAuthService
{
    Task<ErrorOr<UserDto>> SignUp(RegisterDto signUpDto);
    Task<ErrorOr<UserAuthDto>> SignIn(LoginDto login);
    Task<ErrorOr<TokensDto>> RefreshToken(TokensDto expiredToken);
}

public class AuthService(
    ITokenService tokenService,
    IRefreshTokenService refreshTokenService,
    UserManager<UserEntity> userManager)
    : IAuthService
{
    public async Task<ErrorOr<UserDto>> SignUp(RegisterDto signUpDto)
    {
        if (await userManager.FindByEmailAsync(signUpDto.Email) is not null)
        {
            return AuthErrors.Duplicate(signUpDto.Email);
        }

        var result = await userManager.CreateAsync(
            new UserEntity()
            {
                UserName = signUpDto.Name,
                Name = signUpDto.Name,
                Email = signUpDto.Email,
            },
            signUpDto.Password
        );

        if (result.Succeeded)
            return new UserDto()
            {
                Name = signUpDto.Name,
                Email = signUpDto.Email
            };
        
        var stringBuilder = new StringBuilder();
        foreach (var identityError in result.Errors)
            stringBuilder.AppendLine(identityError.Description);

        return AuthErrors.SigUpFailed(stringBuilder.ToString());

    }

    public async Task<ErrorOr<UserAuthDto>> SignIn(LoginDto login)
    {
        var user = await userManager.FindByEmailAsync(login.Login);

        if (user is null || !await userManager.CheckPasswordAsync(user, login.Password))
        {
            return AuthErrors.LoginFailed;
        }

        var refreshToken = await refreshTokenService.GenerateRefreshToken(user);

        return new UserAuthDto()
        {
            Tokens = new TokensDto()
            {
                AccessToken = await tokenService.GenerateAccessToken(user),
                RefreshToken = refreshToken.TokenHash
            },
            User = new UserDto()
            {
                Name = user.Name,
                Email = user.Email!
            }
        };
    }
    
    public async Task<ErrorOr<TokensDto>> RefreshToken(TokensDto expiredToken)
    {
        return await refreshTokenService.RefreshToken(expiredToken);
    }
}