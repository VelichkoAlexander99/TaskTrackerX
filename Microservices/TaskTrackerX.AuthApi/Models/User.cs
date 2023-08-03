using Microsoft.AspNetCore.Identity;

namespace TaskTrackerX.AuthApi.Models
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; } = default!;

        public bool IsArchival { get; set; } = false;

        public virtual IEnumerable<IdentityRole<Guid>> Roles { get; set; } = new List<IdentityRole<Guid>>();
    }
}
