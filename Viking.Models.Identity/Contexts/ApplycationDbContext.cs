using Microsoft.EntityFrameworkCore;

namespace Viking.Models.Contexts;

public class ApplycationDbContext: DbContext
{
    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; } = null!;
    
    public ApplycationDbContext(DbContextOptions<ApplycationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}