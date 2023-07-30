using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TaskTrackerX.AuthApi.DTOs.Outgoing;

namespace TaskTrackerX.AuthApi.MappingProfiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole, RoleDto>();
        }
    }
}
