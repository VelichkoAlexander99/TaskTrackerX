using System.Security.Claims;
using TaskTrackerX.AuthApi.Models;

namespace TaskTrackerX.AuthApi.Services.JwtTokenGenerator
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user, IEnumerable<string> userRoles);
        ClaimsPrincipal GetPrincipalFromToken(string token);
    }
}