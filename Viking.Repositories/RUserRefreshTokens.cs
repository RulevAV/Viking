using Microsoft.EntityFrameworkCore;
using Viking.Interfaces;
using Viking.Models;
using Viking.Models.Contexts;

namespace Viking.Repositories;

public class RUserRefreshTokens: IUserRefreshTokens
{
    private readonly conViking _applycationDbContext;
    
    public RUserRefreshTokens(conViking applycationDbContext)
    {
        _applycationDbContext = applycationDbContext;
    }
    
    public async Task<UserRefreshToken?> GetUserRefreshToken(string userId, string refreshToken)
    {
        var userRefreshToken = await _applycationDbContext.UserRefreshTokens
            .FirstOrDefaultAsync(t => t.UserId == Guid.Parse(userId) && t.RefreshToken == refreshToken);

        return userRefreshToken;
    }
}