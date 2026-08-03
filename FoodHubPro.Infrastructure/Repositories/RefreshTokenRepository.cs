using FoodHubPro.Application.Interfaces;
using FoodHubPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodHubPro.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly FoodHubDbContext _dbContext;

        public RefreshTokenRepository(FoodHubDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RefreshToken?> GetValidTokenAsync(string token, CancellationToken cancellationToken)
        {
            return await _dbContext.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == token && !rt.IsRevoked, cancellationToken);
        }

        public async Task SaveAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
        {
            await _dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task RevokeAsync(string token, CancellationToken cancellationToken)
        {
            var rt = await _dbContext.RefreshTokens.FirstOrDefaultAsync(r => r.Token == token, cancellationToken);
            if (rt != null)
            {
                rt.IsRevoked = true;
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
