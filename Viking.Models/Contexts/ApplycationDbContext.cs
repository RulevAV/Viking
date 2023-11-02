using Microsoft.EntityFrameworkCore;

namespace Viking.Models.IdentityModels;

public class ApplycationDbContext: DbContext
{
    public DbSet<UserRefreshTokens> UserRefreshTokens { get; set; } = null!;
    
    public ApplycationDbContext(DbContextOptions<ApplycationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}