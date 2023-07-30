namespace TaskTrackerX.AuthApi.DTOs.Outgoing
{
    public class AccessTokenDto
    {
        public string Token { get; set; } = default!;
        public int ExpiresInMinutes { get; set; } = default!;
    }
}
