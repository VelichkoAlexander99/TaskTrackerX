using Microsoft.AspNetCore.Identity;

namespace TaskTrackerX.AuthApi.Models
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; } = default!;

        public string? RoleName { get; set; }

        public bool IsArchival { get; set; } = false;
    }
}
