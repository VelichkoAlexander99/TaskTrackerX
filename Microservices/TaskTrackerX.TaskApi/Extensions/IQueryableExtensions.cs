using Microsoft.EntityFrameworkCore;
using TaskTrackerX.TaskApi.Models.Query;

namespace TaskTrackerX.TaskApi.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyFiltering<T>(this IQueryable<T> query, FilterOptions<T> filterOptions)
        {
            if (filterOptions.Filter != null)
            {
                query = query.Where(filterOptions.Filter);
            }

            if (filterOptions.OrderBy != null)
            {
                query = filterOptions.OrderBy(query);
            }

            return query;
        }

        public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query, FilterOptions<T> filterOptions)
        {
            var items = await query
                .ApplyFiltering(filterOptions)
                .Skip((filterOptions.PageNumber - 1) * filterOptions.PageSize)
                .Take(filterOptions.PageSize)
                .ToListAsync();
            
            return new PagedResult<T>
            {
                TotalCount = items.Count,
                PageSize = filterOptions.PageSize,
                CurrentPage = filterOptions.PageNumber,
                TotalPages = (int)Math.Ceiling(items.Count / (double)filterOptions.PageSize),
                Items = items
            };
        }
    }

}
