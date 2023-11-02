using Viking.Models;

namespace Viking.Interfaces;

public interface IUserRefreshTokens
{
    public Task<UserRefreshToken?> GetUserRefreshToken(string userId, string refreshToken);
}