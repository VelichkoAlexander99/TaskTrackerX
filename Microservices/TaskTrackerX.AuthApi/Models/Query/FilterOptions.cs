using System.Linq.Expressions;

namespace TaskTrackerX.AuthApi.Models.Query
{
    public class FilterOptions<T>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 1;
        public Expression<Func<T, bool>>? Filter { get; set; }
        public Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy { get; set; }
    }
}
