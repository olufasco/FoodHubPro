using FoodHubPro.Application.CQRS.Auth.Commands;
using FoodHubPro.Application.DTOs;
using FoodHubPro.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHubPro.Application.CQRS.Auth.Handlers
{
    public class RevokeRefreshTokenCommandHandler : IRequestHandler<RevokeRefreshTokenCommand, AuthResponse>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepo;

        public RevokeRefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepo)
        {
            _refreshTokenRepo = refreshTokenRepo;
        }

        public async Task<AuthResponse> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            await _refreshTokenRepo.RevokeAsync(request.RefreshToken, cancellationToken);

            return new AuthResponse
            {
                Success = true,
                Message = "Refresh token revoked successfully."
            };
        }
    }
}
