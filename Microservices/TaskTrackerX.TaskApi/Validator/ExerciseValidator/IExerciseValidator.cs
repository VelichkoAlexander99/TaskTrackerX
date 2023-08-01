using TaskTrackerX.TaskApi.Managers.ExerciseManager;
using TaskTrackerX.TaskApi.Managers.StatusManager;
using TaskTrackerX.TaskApi.Models;

namespace TaskTrackerX.TaskApi.Validator.ExerciseValidator
{
    public interface IExerciseValidator
    {
        Task<Result> ValidateAsync(Exercise exercise);
    }
}
