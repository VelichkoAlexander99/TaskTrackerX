using Microsoft.AspNetCore.Identity;

namespace TaskTrackerX.AuthApi.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
    }
}
