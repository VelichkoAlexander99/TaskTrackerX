using Microsoft.AspNetCore.Identity;
using TaskTrackerX.AuthApi.Models;

namespace TaskTrackerX.AuthApi.Extensions
{
    public static class IdentityErrorExtensions
    {
        public static ErrorInfo[] ConvertToErrorInfo(this IEnumerable<IdentityError> errors)
        {
            return errors.Select(t => new ErrorInfo()
            {
                Code = t.Code,
                Description = t.Description
            }).ToArray();
        }
    }
}
