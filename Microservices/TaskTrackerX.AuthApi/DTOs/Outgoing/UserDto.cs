namespace TaskTrackerX.AuthApi.DTOs.Outgoing
{
    public class UserDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Login { get; set; }
        public string? RoleName { get; set; }
    }
}
