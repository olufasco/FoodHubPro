using FoodHubPro.Application.DTOs;
using FoodHubPro.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
{
    private readonly IRefreshTokenRepository _refreshTokenRepo;
    private readonly IConfiguration _configuration;

    public RefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepo, IConfiguration configuration)
    {
        _refreshTokenRepo = refreshTokenRepo;
        _configuration = configuration;
    }

    public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var storedToken = await _refreshTokenRepo.GetValidTokenAsync(request.RefreshToken, cancellationToken);

        if (storedToken == null || storedToken.ExpiresAt < DateTime.UtcNow)
        {
            return new AuthResponse { Success = false, Message = "Invalid or expired refresh token." };
        }

        var jwtKey = _configuration["Jwt:Key"];
        if (string.IsNullOrEmpty(jwtKey))
        {
            throw new InvalidOperationException("JWT Key is not configured.");
        }

        var key = Encoding.UTF8.GetBytes(jwtKey);
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, storedToken.UserId.ToString()),
            new Claim(ClaimTypes.Role, "User") // replace with actual role lookup
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };

        var newToken = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(newToken);

        return new AuthResponse
        {
            Success = true,
            Message = "Token refreshed successfully.",
            Token = jwt,
            RefreshToken = request.RefreshToken
        };
    }
}

