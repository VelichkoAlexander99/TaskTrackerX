using Microsoft.AspNetCore.Identity;

namespace TaskTrackerX.AuthApi.Models
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public virtual User? User { get; set; }
        public virtual IdentityRole<Guid>? Role { get; set; }
    }
}
