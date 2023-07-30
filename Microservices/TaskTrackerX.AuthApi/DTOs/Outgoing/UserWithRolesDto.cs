namespace TaskTrackerX.AuthApi.DTOs.Outgoing
{
    public class UserWithRolesDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Login { get; set; }
        public IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();
    }
}
