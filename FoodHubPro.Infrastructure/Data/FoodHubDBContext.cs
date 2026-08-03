using FoodHubPro.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class FoodHubDbContext
    : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public FoodHubDbContext(DbContextOptions<FoodHubDbContext> options) : base(options) { }

    public DbSet<Vendor> Vendors => Set<Vendor>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Vendor>().HasKey(x => x.Id);
        builder.Entity<RefreshToken>().HasKey(x => x.Id);
    }
}
