using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Viking.Models;
using Viking.Models.JWTModels;

namespace Viking.Interfaces;

public interface ITokenService
{
    AccessToken GenerateAccessToken(List<Claim> claims);
    UserRefreshToken? GenerateRefreshToken(Guid userId);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    Task<int> AddRefreshTokensToBase(Guid userId, UserRefreshToken? refreshToken);
    public IEnumerable<Claim> GetClaims();
    Task<UserRefreshToken> GetRefreshToken(string userId,string refreshToken);
    public Task<int> DeleteRefreshTokensToBase(Guid userId, string refreshToken);
    public Task<UserRefreshToken> GetUserIdByRefreshToken (string refreshToken);
}