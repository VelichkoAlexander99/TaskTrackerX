using Microsoft.EntityFrameworkCore;
using TaskTrackerX.TaskApi.Data.Stores.StatusStore;
using TaskTrackerX.TaskApi.Models;

namespace TaskTrackerX.TaskApi.Data.Stores.ExerciseStore
{
    public class ExerciseStore : StoreBase<Exercise>, IExerciseStore
    {
        public ExerciseStore(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<Exercise> Exercise => Context.Set<Exercise>();

        public Task<Exercise?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return Exercise.FirstOrDefaultAsync(s => s.Id.Equals(id), cancellationToken);
        }
    }
}
