namespace FoodHubPro.Application.DTOs
{
    public class AuthResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();
        public string Token { get; set; } = string.Empty; // JWT
        public string RefreshToken { get; set; } = string.Empty; // New
    }
}
