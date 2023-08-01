using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskTrackerX.TaskApi.DTOs.Incoming;
using TaskTrackerX.TaskApi.Models;
using TaskTrackerX.TaskApi.Models.Query;

namespace TaskTrackerX.TaskApi.Managers.ExerciseManager
{
    public interface IExerciseManager
    {
        Task<PagedResult<Exercise>> GetListAsync(FilterOptions<Exercise> filterOptions);
        Task<Exercise?> FindByIdAsync(Guid id);
        Task<Result> CreateAsync(Exercise exercise);
        Task<Result> UpdateAsync(Exercise exercise);
        Task<Result> DeleteAsync(Exercise exercise);
    }
}
