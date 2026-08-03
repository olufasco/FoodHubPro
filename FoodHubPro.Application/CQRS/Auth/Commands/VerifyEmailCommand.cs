using FoodHubPro.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHubPro.Application.CQRS.Auth.Commands
{
    public class VerifyEmailCommand : IRequest<AuthResponse>
    {
        public string UserId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
