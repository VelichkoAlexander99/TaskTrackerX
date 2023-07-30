namespace TaskTrackerX.AuthApi.DTOs.Incoming
{
    public class LoginDto
    {
        public string Login { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
