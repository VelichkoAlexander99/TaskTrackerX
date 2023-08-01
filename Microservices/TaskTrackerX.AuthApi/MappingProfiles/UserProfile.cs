using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TaskTrackerX.AuthApi.DTOs.Incoming;
using TaskTrackerX.AuthApi.DTOs.Outgoing;
using TaskTrackerX.AuthApi.Models;

namespace TaskTrackerX.AuthApi.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.UserName, u => u.MapFrom(src => src.Login));

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Login, u => u.MapFrom(src => src.UserName));
        }
    }
}
