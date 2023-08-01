using TaskTrackerX.TaskApi.Models.Query;
using TaskTrackerX.TaskApi.Models;
using Microsoft.Extensions.Options;
using TaskTrackerX.TaskApi.Models.Options;
using System.IO;
using TaskTrackerX.TaskApi.DTOs.Incoming;

namespace TaskTrackerX.TaskApi.Extensions
{
    public static class FilterOptionsExtensions
    {
        public static FilterOptions<Status> ConvertToFilterOptions(this StatusParametersDto parameters, IOptions<SettingOptions> options)
        {
            var filterOptions = new FilterOptions<Status>();

            if (!string.IsNullOrEmpty(parameters.Name))
                filterOptions.Filter = status => status.Name.Contains(parameters.Name);

            filterOptions.SetPaging(parameters, options);

            return filterOptions;
        }

        public static FilterOptions<Exercise> ConvertToFilterOptions(this ExerciseParametersDto parameters, IOptions<SettingOptions> options)
        {
            var filterOptions = new FilterOptions<Exercise>();

            if (parameters.ExerciseStatusId.HasValue)
                filterOptions.Filter = exercise => exercise.ExerciseStatusId.Equals(parameters.ExerciseStatusId.Value);

            if (parameters.CreatedByUserId.HasValue)
                filterOptions.Filter = exercise => exercise.CreatedByUserId.Equals(parameters.CreatedByUserId.Value);

            if (parameters.AssignedToUserId.HasValue)
                filterOptions.Filter = exercise => exercise.AssignedToUserId.Equals(parameters.AssignedToUserId.Value);

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
