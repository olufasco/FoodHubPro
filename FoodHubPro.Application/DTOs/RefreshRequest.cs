namespace FoodHubPro.Application.DTOs
{
    public class RefreshRequest
    {
        public string Token { get; set; } = string.Empty; // The expired JWT
        public string RefreshToken { get; set; } = string.Empty; // The refresh token issued at login
    }
}
