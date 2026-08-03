using Microsoft.AspNetCore.Identity;
using FoodHubPro.Domain.Enums;

namespace FoodHubPro.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FullName { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; }
            = new List<RefreshToken>();
    }
}
