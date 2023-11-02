using Microsoft.EntityFrameworkCore;
using Viking.Interfaces;
using Viking.Models;

namespace Viking.Repositories;

public class UserRefreshTokensRepositories: IUserRefreshTokens
{
    private readonly conViking _applycationDbContext;
    
    public UserRefreshTokensRepositories(conViking applycationDbContext)
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