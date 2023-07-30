namespace TaskTrackerX.AuthApi.DTOs.Incoming
{
    public class UpdateUserDto
    {
        public string? Name { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
    }
}
