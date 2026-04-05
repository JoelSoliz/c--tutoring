namespace WatchTrackerAPI.DTOs.Responses
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
    }
}
