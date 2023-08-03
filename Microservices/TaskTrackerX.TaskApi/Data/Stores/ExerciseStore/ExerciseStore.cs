using Microsoft.EntityFrameworkCore;
using System.Threading;
using TaskTrackerX.TaskApi.Data.Stores.StatusStore;
using TaskTrackerX.TaskApi.Models;

namespace TaskTrackerX.TaskApi.Data.Stores.ExerciseStore
{
    public class ExerciseStore : StoreBase<Exercise>, IExerciseStore
    {
        public ExerciseStore(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<Exercise> Exercises => Context.Set<Exercise>();

        public async Task<Result> ArchiveByUserId(Guid userId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await Exercises.Where(e =>
                    e.AssignedToUserId.Equals(userId) ||
                    e.CreatedByUserId.Equals(userId))
                .ForEachAsync(entity => entity.IsArchival = true);

            await Context.SaveChangesAsync(cancellationToken);
            return Result.Success;
        }

        public Task<Exercise?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return Exercises.FirstOrDefaultAsync(s => s.Id.Equals(id), cancellationToken);
        }
    }
}
