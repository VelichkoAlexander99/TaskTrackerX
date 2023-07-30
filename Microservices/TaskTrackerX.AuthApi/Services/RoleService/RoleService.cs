using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskTrackerX.AuthApi.DTOs.Outgoing;
using TaskTrackerX.AuthApi.Models;

namespace TaskTrackerX.AuthApi.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleService(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<RoleDto>>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return ApiResponse<IEnumerable<RoleDto>>.Success(
                _mapper.Map<IEnumerable<RoleDto>>(roles));
        }
    }
}
