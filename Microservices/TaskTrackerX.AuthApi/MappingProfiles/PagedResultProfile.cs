using AutoMapper;
using TaskTrackerX.AuthApi.Models.Query;

namespace TaskTrackerX.AuthApi.MappingProfiles
{
    public class PagedResultProfile : Profile
    {
        public PagedResultProfile()
        {
            CreateMap(typeof(PagedResult<>), typeof(PagedResult<>));
        }
    }
}
