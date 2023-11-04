using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Viking.Interfaces;
using Viking.Models;
using Viking.Models.JWTModels;

namespace Viking.Repositories;

public class TokenServices : ITokenService
{
    private readonly JWTSettings _options;
    private readonly conViking _applycationDbContext;
    private readonly UserRefreshTokensRepositories _userRefreshTokensRepositories ;
    private IEnumerable<Claim> _claims;

    public TokenServices(IOptions<JWTSettings> options,conViking applycationDbContext)
    {
        _options = options.Value;
        _userRefreshTokensRepositories = new UserRefreshTokensRepositories(applycationDbContext);
        _applycationDbContext = applycationDbContext;
    }

    public void AddClaims(IdentityUser user,IEnumerable<Claim> principal)
    {
        var claims = principal.ToList();
        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
        claims.Add(new Claim("idUser", user.Id));

        _claims = claims;
    }

    public IEnumerable<Claim> GetClaims()
    {
        return _claims;
    }
    
    public string GenerateAccessToken()
    {
        var claims = GetClaims();
        var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

        var jwt = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(1)),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    [Obsolete("Obsolete")]
    public UserRefreshToken GenerateRefreshToken(Guid userId)
    {
        var randomNumber = new byte[32];
        using (var generator = new RNGCryptoServiceProvider())
        {
            generator.GetBytes(randomNumber);
            return new UserRefreshToken
            {
                RefreshToken = Convert.ToBase64String(randomNumber),
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(10),
                RefreshTokenCreatedTime = DateTime.UtcNow,
                UserId = userId
            };
        }
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345")),
            ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");
        return principal;
    }

    public async Task<int> AddRefreshTokensToBase(Guid userId,UserRefreshToken? refreshToken)
    {
         var userRefreshToken = new UserRefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                RefreshToken = refreshToken.RefreshToken,
                RefreshTokenExpiryTime = refreshToken.RefreshTokenExpiryTime,
                RefreshTokenCreatedTime = refreshToken.RefreshTokenCreatedTime
            };

         await _applycationDbContext.UserRefreshTokens.AddAsync(userRefreshToken);
         return await _applycationDbContext.SaveChangesAsync();
    }

    public async Task<UserRefreshToken> GetUserIdByRefreshToken(string refreshToken)
    {
        return await _applycationDbContext.UserRefreshTokens.FirstOrDefaultAsync(t => t.RefreshToken == refreshToken);
    }
    
    
    public async Task<int> DeleteRefreshTokensToBase(Guid userId,string refreshToken)
    {
        var userRefreshToken = _applycationDbContext.UserRefreshTokens.FirstOrDefault(t => t.RefreshToken == refreshToken);

        if (userRefreshToken != null)
            _applycationDbContext.UserRefreshTokens.Remove(userRefreshToken);
        
        return await _applycationDbContext.SaveChangesAsync();
    } 
    
    public async Task<UserRefreshToken> GetRefreshToken(string userId,string refreshToken)
    {
        var newRefreshToken = await _userRefreshTokensRepositories.GetUserRefreshToken(userId, refreshToken);

        return newRefreshToken;
    }
}