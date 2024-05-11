using Booking.Infrastructure.Auth;
using Booking.Infrastructure.Database.Abstractions;
using Booking.Infrastructure.User;
using ErrorOr;
using Microsoft.AspNetCore.Identity;

namespace Booking.Core.Auth;

public interface IRefreshTokenService
{
    Task<RefreshTokenEntity> GenerateRefreshToken(UserEntity userEntity);
    Task<ErrorOr<TokensDto>> RefreshToken(TokensDto expiredToken);
}

public class RefreshTokenService(ITokenService tokenService, IRefreshTokenRepository refreshTokenRepository, UserManager<UserEntity> userManager)
    : IRefreshTokenService
{
    public async Task<RefreshTokenEntity> GenerateRefreshToken(UserEntity userEntity)
    {
        var refreshToken = new RefreshTokenEntity
        {
            TokenHash = await tokenService.GenerateRefreshToken(),
            CreatedDate = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddDays(30),
            UserId = userEntity.Id,
            UserEntity = userEntity
        };

        await refreshTokenRepository.Create(refreshToken);

        return refreshToken;
    }
    
    public async Task<ErrorOr<TokensDto>> RefreshToken(TokensDto expiredToken)
    {
        var principals = tokenService.GetPrincipalFromExpiredToken(expiredToken.AccessToken);
        var user = await userManager.FindByNameAsync(principals.Identity.Name);

        if (user is null)
        {
            return AuthErrors.ValidationFailed;
        }
        
        var refreshToken = await refreshTokenRepository.GetActiveByUserId(user.Id);
        
        if (refreshToken is null)
        {
            return AuthErrors.ValidationFailed;
        }

        return new TokensDto()
        {
            AccessToken = await tokenService.GenerateAccessToken(user),
            RefreshToken = refreshToken.TokenHash
        };
    }
}