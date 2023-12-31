﻿using Microsoft.EntityFrameworkCore;
using TaskTrackerX.TaskApi.Data.Stores.StatusStore;
using TaskTrackerX.TaskApi.Extensions;
using TaskTrackerX.TaskApi.Models;
using TaskTrackerX.TaskApi.Models.Query;
using TaskTrackerX.TaskApi.Validator.StatusValidator;

namespace TaskTrackerX.TaskApi.Managers.StatusManager
{
    public class StatusManager : IStatusManager
    {
        private readonly IStatusStore _statusStore;
        private readonly IStatusValidator _statusValidator;

        public StatusManager(
            IStatusStore store,
            IStatusValidator statusValidator)
        {
            _statusStore = store;
            _statusValidator = statusValidator;
        }

        public async Task<PagedResult<Status>> GetListAsync(FilterOptions<Status> filterOptions)
        {
            return await _statusStore.Status
                .GetPagedAsync(filterOptions);
        }

        public async Task<Status?> FindByIdAsync(Guid id)
        {
            return await _statusStore.FindByIdAsync(id);
        }

        public async Task<Status?> FindByNameAsync(string statusName)
        {
            if (statusName == null)
                throw new ArgumentNullException(nameof(statusName));

            return await _statusStore.FindByNameAsync(statusName);
        }

        public async Task<Result> CreateAsync(Status status)
        {
            if (status == null)
                throw new ArgumentNullException(nameof(status));

            var result = await _statusValidator.ValidateAsync(this, status);

            return result.Succeeded ? await _statusStore.CreateAsync(status) : result;
        }

        public async Task<Result> UpdateAsync(Status status)
        {
            if (status == null)
                throw new ArgumentNullException(nameof(status));

            var result = await _statusValidator.ValidateAsync(this, status);
            
            return result.Succeeded ? await _statusStore.UpdateAsync(status) : result;
        }

        public async Task<Result> DeleteAsync(Status status)
        {
            if (status == null)
                throw new ArgumentNullException(nameof(status));

            return await _statusStore.DeleteAsync(status);
        }
    }
}
