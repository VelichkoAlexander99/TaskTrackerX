using Microsoft.EntityFrameworkCore;
using TaskTrackerX.TaskApi.Data.Stores.ExerciseStore;
using TaskTrackerX.TaskApi.Extensions;
using TaskTrackerX.TaskApi.Models;
using TaskTrackerX.TaskApi.Models.Query;
using TaskTrackerX.TaskApi.Validator.ExerciseValidator;

namespace TaskTrackerX.TaskApi.Managers.ExerciseManager
{
    public class ExerciseManager : IExerciseManager
    {
        private readonly IExerciseStore _exerciseStore;
        private readonly IExerciseValidator _exerciseValidator;

        public ExerciseManager(
            IExerciseStore exerciseStore,
            IExerciseValidator exerciseValidator)
        {
            _exerciseStore = exerciseStore;
            _exerciseValidator = exerciseValidator;
        }

        public async Task<PagedResult<Exercise>> GetListAsync(FilterOptions<Exercise> filterOptions)
        {
            return await _exerciseStore.Exercises
                .GetPagedAsync(filterOptions);
        }

        public Task<Exercise?> FindByIdAsync(Guid id)
        {
            return _exerciseStore.FindByIdAsync(id);
        }

        public async Task<Result> CreateAsync(Exercise exercise)
        {
            if (exercise == null)
                throw new ArgumentNullException(nameof(exercise));

            var result = await _exerciseValidator.ValidateAsync(exercise);

            return result.Succeeded ? await _exerciseStore.CreateAsync(exercise) : result;
        }

        public async Task<Result> UpdateAsync(Exercise exercise)
        {
            if (exercise == null)
                throw new ArgumentNullException(nameof(exercise));

            var result = await _exerciseValidator.ValidateAsync(exercise);

            return result.Succeeded ? await _exerciseStore.UpdateAsync(exercise) : result;
        }

        public async Task<Result> DeleteAsync(Exercise exercise)
        {
            if (exercise == null)
                throw new ArgumentNullException(nameof(exercise));

            return await _exerciseStore.DeleteAsync(exercise);
        }

        public Task<Result> ArchiveByUserId(Guid userId)
        {
            return _exerciseStore.ArchiveByUserId(userId);
        }
    }
}
