using Microsoft.AspNetCore.Identity;
using FoodHubPro.Domain.Enums;

namespace FoodHubPro.Infrastructure.Identity;

public class ApplicationUserIdentity : IdentityUser<Guid>
{
    public string FullName { get; set; } = string.Empty;

    public UserRole Role { get; set; }

    public bool IsVerified { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }
}