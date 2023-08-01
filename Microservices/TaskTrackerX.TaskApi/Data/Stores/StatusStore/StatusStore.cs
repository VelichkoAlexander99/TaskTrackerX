using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using TaskTrackerX.TaskApi.Models;
using TaskTrackerX.TaskApi.Services;

namespace TaskTrackerX.TaskApi.Data.Stores.StatusStore
{
    public class StatusStore : StoreBase<Status>, IStatusStore
    {
        public StatusStore(ApplicationDbContext context) : base(context)
        {}

        public IQueryable<Status> Status => Context.Set<Status>();

        public Task<Status?> FindByIdAsync(Guid id, 
            CancellationToken cancellationToken = default)
        {
            return Status.FirstOrDefaultAsync(s => s.Id.Equals(id), cancellationToken);
        }

        public Task<Status?> FindByNameAsync(string statusName, CancellationToken cancellationToken = default)
        {
            return Status.FirstOrDefaultAsync(s => s.Name == statusName, cancellationToken);
        }
    }
}
