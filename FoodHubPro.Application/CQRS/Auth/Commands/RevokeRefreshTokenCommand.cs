using FoodHubPro.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHubPro.Application.CQRS.Auth.Commands
{
    public class RevokeRefreshTokenCommand : IRequest<AuthResponse>
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
