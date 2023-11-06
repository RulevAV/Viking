namespace Viking.Models.JWTModels;

public class TokenApiModel
{
    public AccessToken? AccessToken { get; set; }
    public RefreshToken? RefreshToken { get; set; }
}