using AutoMapper;
using TaskTrackerX.TaskApi.DTOs.Incoming;
using TaskTrackerX.TaskApi.DTOs.Outgoing;
using TaskTrackerX.TaskApi.Models;

namespace TaskTrackerX.TaskApi.MappingProfiles
{
    public class StatusProfile : Profile
    {
        public StatusProfile()
        {
            CreateMap<Status, StatusDto>();
            CreateMap<StatusCreateUpdateDto, Status>();
        }
    }
}
