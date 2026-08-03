using FoodHubPro.Domain.Entities;

namespace FoodHubPro.Application.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetValidTokenAsync(string token, CancellationToken cancellationToken);
        Task SaveAsync(RefreshToken refreshToken, CancellationToken cancellationToken);
        Task RevokeAsync(string token, CancellationToken cancellationToken);
    }
}
