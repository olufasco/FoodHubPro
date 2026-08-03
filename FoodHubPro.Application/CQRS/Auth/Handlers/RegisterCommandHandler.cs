using MediatR;
using Microsoft.AspNetCore.Identity;
using FoodHubPro.Domain.Entities;
using FoodHubPro.Application.CQRS.Auth.Commands;
using FoodHubPro.Application.DTOs;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public RegisterCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            FullName = request.FullName,
            Email = request.Email,
            UserName = request.Email,
            PhoneNumber = request.PhoneNumber,
            Role = request.Role, // your enum or string
            IsVerified = false,
            CreatedAt = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return new AuthResponse
            {
                Success = false,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }

        // Assign role to user
        await _userManager.AddToRoleAsync(user, request.Role.ToString());

        // Generate email verification token
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        // TODO: Send email with verification link

        return new AuthResponse
        {
            Success = true,
            Message = "Registration successful. Please verify your email.",
            Token = token
        };
    }

}
