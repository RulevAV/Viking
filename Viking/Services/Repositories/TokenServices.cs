using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Viking.Models.IdentityModels;
using Viking.Models.JWTModels;

namespace Viking.Services.Repositories;

public class TokenServices : ITokenService
{
    private readonly JWTSettings _options;
    private readonly ApplycationDbContext _applycationDbContext;

    public TokenServices(IOptions<JWTSettings> options,ApplycationDbContext applycationDbContext)
    {
        _options = options.Value;
        _applycationDbContext = applycationDbContext;
    }
    
    public string GenerateAccessToken(IdentityUser user, IEnumerable<Claim> principal)
    {
        
        var claims = principal.ToList();
        claims.Add(new Claim(ClaimTypes.Name, user.UserName));

        var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

        var jwt = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    [Obsolete("Obsolete")]
    public RefreshToken GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var generator = new RNGCryptoServiceProvider())
        {
            generator.GetBytes(randomNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                Expires = DateTime.UtcNow.AddDays(10),
                Created = DateTime.UtcNow
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

    public async Task<int> AddRefreshTokensToBase(Guid userId,RefreshToken refreshToken)
    {
         var userRefreshToken = new UserRefreshTokens
            {
                UserId = userId,
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiryTime = refreshToken.Expires,
                RefreshTokenCreatedTime = refreshToken.Created
            };
            return await _applycationDbContext.SaveChangesAsync();
    }
    
}