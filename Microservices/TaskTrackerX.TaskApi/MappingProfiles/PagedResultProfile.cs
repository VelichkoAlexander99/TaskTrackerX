using AutoMapper;
using TaskTrackerX.TaskApi.Models.Query;

namespace TaskTrackerX.TaskApi.MappingProfiles
{
    public class PagedResultProfile : Profile
    {
        public PagedResultProfile()
        {
            CreateMap(typeof(PagedResult<>), typeof(PagedResult<>));
        }
    }
}
