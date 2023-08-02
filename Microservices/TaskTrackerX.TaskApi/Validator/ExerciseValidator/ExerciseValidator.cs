using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using TaskTrackerX.TaskApi.Managers.ExerciseManager;
using TaskTrackerX.TaskApi.Managers.StatusManager;
using TaskTrackerX.TaskApi.Models;
using TaskTrackerX.TaskApi.Services;
using TaskTrackerX.TaskApi.Services.UserService;

namespace TaskTrackerX.TaskApi.Validator.ExerciseValidator
{
    public class ExerciseValidator : IExerciseValidator
    {
        private readonly IStatusManager _statusManager;
        private readonly IUserService _userService;

        public ExerciseValidator(
            IStatusManager statusManager,
            IUserService userService)
        {
            _statusManager = statusManager;
            _userService = userService;
        }

        public async Task<Result> ValidateAsync(Exercise exercise)
        {
            if (exercise == null)
                throw new ArgumentNullException(nameof(exercise));

            var errors = new List<ErrorInfo>();

            await ValidateDescription(exercise, errors);
            await ValidateSubject(exercise, errors);
            await ValidateReceivedDateAndDeadline(exercise, errors);
            await ValidateExerciseStatus(exercise, errors);
            await ValidateAssignedToUser(exercise, errors);

            if (errors.Count > 0)
                return Result.Failed(errors.ToArray());

            return Result.Success;
        }

        private Task ValidateDescription(Exercise exercise, List<ErrorInfo> errors)
        {
            const int MIN_LENGTH = 5;
            const int MAX_LENGTH = 100;

            if (string.IsNullOrWhiteSpace(exercise.Description))
                errors.Add(ErrorDescriber.InvalidExerciseDescription());

            if (exercise.Description.Length < MIN_LENGTH)
                errors.Add(ErrorDescriber.InvalidExerciseDescriptionMinLength(MIN_LENGTH, exercise.Description.Length));

            if (exercise.Description.Length > MAX_LENGTH)
                errors.Add(ErrorDescriber.InvalidExerciseDescriptionMaxLength(MAX_LENGTH, exercise.Description.Length));

            return Task.CompletedTask;
        }

        private Task ValidateSubject(Exercise exercise, List<ErrorInfo> errors)
        {
            const int MIN_LENGTH = 5;
            const int MAX_LENGTH = 35;

            if (string.IsNullOrWhiteSpace(exercise.Subject))
                errors.Add(ErrorDescriber.InvalidExerciseSubject());

            if (exercise.Subject.Length < MIN_LENGTH)
                errors.Add(ErrorDescriber.InvalidExerciseSubjectMinLength(MIN_LENGTH, exercise.Subject.Length));

            if (exercise.Subject.Length > MAX_LENGTH)
                errors.Add(ErrorDescriber.InvalidExerciseSubjectMaxLength(MAX_LENGTH, exercise.Subject.Length));

            return Task.CompletedTask;
        }

        private Task ValidateReceivedDateAndDeadline(Exercise exercise, List<ErrorInfo> errors)
        {
            if (exercise.ReceivedDate.CompareTo(exercise.Deadline) >= 0)
                errors.Add(ErrorDescriber.InvalidExerciseReceivedDateAndDeadline(exercise.ReceivedDate, exercise.Deadline));

            return Task.CompletedTask;
        }

        private async Task ValidateExerciseStatus(Exercise exercise, List<ErrorInfo> errors)
        {
            var status = await _statusManager.FindByIdAsync(exercise.ExerciseStatusId);
            if (status == null)
                errors.Add(ErrorDescriber.InvalidStatus());
        }

        private async Task ValidateAssignedToUser(Exercise exercise, List<ErrorInfo> errors)
        {
            var user = await _userService.FindByIdAsync(exercise.AssignedToUserId);
            if (user.HasErrors)
                errors.Add(ErrorDescriber.InvalidUser());
        }
    }
}
