using Microsoft.EntityFrameworkCore;
using TaskTrackerX.AuthApi.Data;
using TaskTrackerX.AuthApi.DTOs.Outgoing;
using TaskTrackerX.AuthApi.Models;

namespace TaskTrackerX.AuthApi.Services
{
    public class UserWithRoleService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserWithRoleService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<User> GetQueryUsersWithRoles()
        {
            var usersWithRoles = _dbContext.Users
                .Join(_dbContext.UserRoles,
                    user => user.Id,
                    userRole => userRole.UserId,
                    (user, userRole) => new { User = user, RoleId = userRole.RoleId })
                .Join(_dbContext.Roles,
                    userRole => userRole.RoleId,
                    role => role.Id,
                    (userRole, role) => new { User = userRole.User, RoleName = role.Name })
                .GroupBy(x => new { x.User.Id, x.User.UserName, x.User.Name })
                .Select(g => new User
                {
                    Id = g.Key.Id,
                    UserName = g.Key.UserName,
                    Name = g.Key.Name,
                    RoleName = g.Select(x => x.RoleName).FirstOrDefault()
                });

            return usersWithRoles;
        }
    }
}
