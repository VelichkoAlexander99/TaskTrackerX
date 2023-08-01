using Microsoft.EntityFrameworkCore;
using TaskTrackerX.AuthApi.Models.Query;

namespace TaskTrackerX.AuthApi.Extensions
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
            var totalCount = await query.ApplyFiltering(filterOptions).CountAsync();
            var items = await query
                .ApplyFiltering(filterOptions)
                .Skip((filterOptions.PageNumber - 1) * filterOptions.PageSize)
                .Take(filterOptions.PageSize)
                .ToListAsync();

            return new PagedResult<T>
            {
                TotalCount = totalCount,
                PageSize = filterOptions.PageSize,
                CurrentPage = filterOptions.PageNumber,
                TotalPages = (int)Math.Ceiling(totalCount / (double)filterOptions.PageSize),
                Items = items
            };
        }
    }
}
