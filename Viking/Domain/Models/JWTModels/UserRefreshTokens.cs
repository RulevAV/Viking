using System.ComponentModel.DataAnnotations;

namespace Viking.Models.IdentityModels;

public class UserRefreshTokens
{
    [Required]
    [Key]
    public Guid Id { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public string? RefreshToken { get; set; }
    [Required]
    public DateTime RefreshTokenExpiryTime { get; set; }
    [Required]
    public DateTime RefreshTokenCreatedTime { get; set; }
}