using Microsoft.EntityFrameworkCore;
using TaskTrackerX.TaskApi.Data.Stores.ExerciseStore;
using TaskTrackerX.TaskApi.Models;
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

        public async Task<IEnumerable<Exercise>> GetListAsync()
        {
            return await _exerciseStore.Exercise
                .ToListAsync();
        }

        public Task<Exercise?> FindByIdAsync(Guid id)
        {
            return _exerciseStore.FindByIdAsync(id);
        }

        public async Task<IEnumerable<Exercise>> GetByAssignedUserIdAsync(Guid userId)
        {
            return await _exerciseStore.Exercise
                .Where(e => e.AssignedToUserId.Equals(userId))
                .ToListAsync();
        }

        public async Task<Result> CreateAsync(Exercise exercise)
        {
            if (exercise == null)
                throw new ArgumentNullException(nameof(exercise));

            var result = await _exerciseValidator.ValidateAsync(exercise);
            if (!result.Succeeded)
                return result;

            return await _exerciseStore.CreateAsync(exercise);
        }

        public async Task<Result> UpdateAsync(Exercise exercise)
        {
            if (exercise == null)
                throw new ArgumentNullException(nameof(exercise));

            var result = await _exerciseValidator.ValidateAsync(exercise);
            if (!result.Succeeded)
                return result;

            return await _exerciseStore.UpdateAsync(exercise);
        }

        public async Task<Result> DeleteAsync(Exercise exercise)
        {
            if (exercise == null)
                throw new ArgumentNullException(nameof(exercise));

            return await _exerciseStore.DeleteAsync(exercise);
        }
    }
}
