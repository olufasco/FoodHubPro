using FoodHubPro.Application.DTOs;
using FoodHubPro.Domain.Enums;
using MediatR;

namespace FoodHubPro.Application.CQRS.Auth.Commands
{
    public class RegisterCommand : IRequest<AuthResponse>
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRole Role { get; set; }
    }
}
