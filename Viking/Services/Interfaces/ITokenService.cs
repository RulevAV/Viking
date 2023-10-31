using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Viking.Models.JWTModels;

namespace Viking.Services;

public interface ITokenService
{
    string GenerateAccessToken(IdentityUser user, IEnumerable<Claim> principal);
    RefreshToken GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

    Task<int> AddRefreshTokensToBase(Guid userId,RefreshToken refreshToken);
}