using FoodHubPro.Application.CQRS.Auth.Commands;
using FoodHubPro.Application.DTOs;
using FoodHubPro.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHubPro.Application.CQRS.Auth.Handlers
{
    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, AuthResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public VerifyEmailCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AuthResponse> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return new AuthResponse { Success = false, Message = "Invalid user." };
            }

            var result = await _userManager.ConfirmEmailAsync(user, request.Token);
            if (!result.Succeeded)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "Email verification failed.",
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }

            return new AuthResponse
            {
                Success = true,
                Message = "Email verified successfully."
            };
        }
    }
}
