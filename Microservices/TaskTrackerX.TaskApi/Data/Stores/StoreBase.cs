using Microsoft.EntityFrameworkCore;
using TaskTrackerX.TaskApi.Models;
using TaskTrackerX.TaskApi.Services;

namespace TaskTrackerX.TaskApi.Data.Stores
{
    public class StoreBase<TEntity> : IStoreBase<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext Context;

        public StoreBase(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<Result> CreateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            Context.Set<TEntity>().Add(entity);

            await Context.SaveChangesAsync(cancellationToken);
            return Result.Success;
        }

        public async Task<Result> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            Context.Set<TEntity>().Remove(entity);

            try
            {
                await Context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Result.Failed(ErrorDescriber.ConcurrencyFailure());
            }

            return Result.Success;
        }

        public async Task<Result> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            Context.Set<TEntity>().Update(entity);

            try
            {
                await Context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Result.Failed(ErrorDescriber.ConcurrencyFailure());
            }

            return Result.Success;
        }
    }
}
