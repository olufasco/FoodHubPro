namespace FoodHubPro.Domain.Entities;

public class Vendor
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string BusinessName { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string? LogoUrl { get; set; }

    public bool IsApproved { get; set; }

    public DateTime CreatedAt { get; set; }

    public ApplicationUser? User { get; set; }
}