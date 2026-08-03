using MediatR;
using FoodHubPro.Application.DTOs;

public class LoginCommand : IRequest<AuthResponse>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
