namespace TaskTrackerX.AuthApi.DTOs.Incoming
{
    public class CreateUserDto
    {
        public string Name { get; set; } = default!;
        public string Login { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
