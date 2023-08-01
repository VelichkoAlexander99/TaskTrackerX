using TaskTrackerX.TaskApi.Models;

namespace TaskTrackerX.TaskApi.Managers.ExerciseManager
{
    public interface IExerciseManager
    {
        Task<IEnumerable<Exercise>> GetListAsync();
        Task<IEnumerable<Exercise>> GetByAssignedUserIdAsync(Guid userId);
        Task<Exercise?> FindByIdAsync(Guid id);
        Task<Result> CreateAsync(Exercise exercise);
        Task<Result> UpdateAsync(Exercise exercise);
        Task<Result> DeleteAsync(Exercise exercise);
    }
}
