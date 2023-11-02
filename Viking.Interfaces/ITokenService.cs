using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Viking.Models;

namespace Viking.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken();
    UserRefreshToken? GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    Task<int> AddRefreshTokensToBase(Guid userId, UserRefreshToken? refreshToken);
    public void AddClaims(IdentityUser user, IEnumerable<Claim> principal);
    public IEnumerable<Claim> GetClaims();
    Task<UserRefreshToken> GetRefreshToken(string userId,string refreshToken);
    public Task<int> DeleteRefreshTokensToBase(Guid userId, string refreshToken);
}