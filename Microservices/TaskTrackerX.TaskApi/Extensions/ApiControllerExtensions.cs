using Microsoft.AspNetCore.Mvc;
using TaskTrackerX.TaskApi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskTrackerX.TaskApi.Extensions
{
    public static class ApiControllerExtensions
    {
        public static IActionResult ToApiResponseError(this ControllerBase controller, int statusCode = 400, params ErrorInfo[] errors)
        {
            var response = new ApiResponse<string>
            {
                Data = default,
                ErrorMessage = errors
            };

            return new ObjectResult(response)
            {
                StatusCode = statusCode
            };
        }

        public static IActionResult ToApiResponse<T>(this ControllerBase controller, T data, int statusCode = 200)
        {
            var response = new ApiResponse<T>
            {
                Data = data,
                ErrorMessage = new List<ErrorInfo>()
            };

            return new ObjectResult(response)
            {
                StatusCode = statusCode
            };
        }
    }
}
