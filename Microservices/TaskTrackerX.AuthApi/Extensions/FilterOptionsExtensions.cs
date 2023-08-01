using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Linq;
using TaskTrackerX.AuthApi.DTOs.Incoming;
using TaskTrackerX.AuthApi.Models;
using TaskTrackerX.AuthApi.Models.Options;
using TaskTrackerX.AuthApi.Models.Query;

namespace TaskTrackerX.AuthApi.Extensions
{
    public static class FilterOptionsExtensions
    {
        public static FilterOptions<User> ConvertToFilterOptions(this UserParametersDto parameters, IOptions<SettingOptions> options)
        {
            var filterOptions = new FilterOptions<User>();

            if (!string.IsNullOrEmpty(parameters.Login))
                filterOptions.Filter = exercise => exercise.UserName != null && exercise.UserName.Contains(parameters.Login);

            if (!string.IsNullOrEmpty(parameters.Name))
                filterOptions.Filter = status => status.Name.Contains(parameters.Name);

            filterOptions.SetPaging(parameters, options);

            return filterOptions;
        }

        public static FilterOptions<IdentityRole<Guid>> ConvertToFilterOptions(this QueryParametersDto parameters, IOptions<SettingOptions> options)
        {
            var filterOptions = new FilterOptions<IdentityRole<Guid>>();

            filterOptions.SetPaging(parameters, options);

            return filterOptions;
        }

        private static void SetPaging<T>(this FilterOptions<T> filterOptions, QueryParametersDto parameters, IOptions<SettingOptions> options)
        {
            filterOptions.PageNumber = parameters.PageNumber;

            if (filterOptions.PageNumber < 1)
                filterOptions.PageNumber = 1;

            filterOptions.PageSize = parameters.PageSize;

            if (parameters.PageSize < 1)
                filterOptions.PageSize = options.Value.PaginationSettings.MaxPageSize;

            if (parameters.PageSize > options.Value.PaginationSettings.MaxPageSize)
                filterOptions.PageSize = options.Value.PaginationSettings.MaxPageSize;
        }
    }
}
