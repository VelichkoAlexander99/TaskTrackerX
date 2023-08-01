using TaskTrackerX.TaskApi.Models;

namespace TaskTrackerX.TaskApi.Data.Stores.ExerciseStore
{
    public interface IExerciseStore : IStoreBase<Exercise>
    {
        IQueryable<Exercise> Exercise { get; }

        Task<Exercise?> FindByIdAsync(Guid id,
            CancellationToken cancellationToken = default);
    }
}
