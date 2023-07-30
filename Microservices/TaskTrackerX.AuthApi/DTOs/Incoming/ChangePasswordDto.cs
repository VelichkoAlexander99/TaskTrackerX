namespace TaskTrackerX.AuthApi.DTOs.Incoming
{
    public class ChangePasswordDto
    {
        public string OldPassword { get; set; } = default!;
        public string NewPassword { get; set; } = default!;
    }
}
