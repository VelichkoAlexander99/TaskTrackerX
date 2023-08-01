using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Data;
using TaskTrackerX.TaskApi.Managers.StatusManager;
using TaskTrackerX.TaskApi.Models;
using TaskTrackerX.TaskApi.Services;

namespace TaskTrackerX.TaskApi.Validator.StatusValidator
{
    public class StatusValidator : IStatusValidator
    {
        public async Task<Result> ValidateAsync(IStatusManager manager, Status status)
        {
            if (manager == null)
                throw new ArgumentNullException(nameof(manager));

            if (status == null)
                throw new ArgumentNullException(nameof(status));

            var errors = new List<ErrorInfo>();

            await ValidateStatusName(manager, status, errors);

            if (errors.Count > 0)
                return Result.Failed(errors.ToArray());

            return Result.Success;
        }

        private async Task ValidateStatusName(IStatusManager manager, Status status,
            ICollection<ErrorInfo> errors)
        {
            if (string.IsNullOrWhiteSpace(status.Name))
                errors.Add(ErrorDescriber.InvalidStatusName(status.Name));
            else
            {
                var owner = await manager.FindByNameAsync(status.Name);
                if (owner != null && !owner.Id.Equals(status.Id))
                    errors.Add(ErrorDescriber.DuplicateRoleName(status.Name));
            }
        }
    }
}
