using TaskTrackerX.TaskApi.Models;

namespace TaskTrackerX.TaskApi.Data.Stores.ExerciseStore
{
    public interface IExerciseStore : IStoreBase<Exercise>
    {
        IQueryable<Exercise> Exercises { get; }

        Task<Result> ArchiveByUserId(Guid userId,
            CancellationToken cancellationToken = default);
        Task<Exercise?> FindByIdAsync(Guid id,
            CancellationToken cancellationToken = default);
    }
}
