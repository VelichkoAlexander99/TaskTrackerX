using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace TaskTrackerX.TaskApi.Models
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public bool HasErrors => ErrorMessage.Any();
        public IEnumerable<ErrorInfo> ErrorMessage { get; set; } = new List<ErrorInfo>();
        public int StatusCode { get; set; }

        public ApiResponse(T data, int statusCode = 200)
        {
            Data = data;
            StatusCode = statusCode;
        }

        public ApiResponse(IEnumerable<ErrorInfo> errors, int statusCode = 400)
        {
            ErrorMessage = errors;
            StatusCode = statusCode;
        }

        public ApiResponse(int statusCode = 400, params ErrorInfo[] errors)
        {
            ErrorMessage = errors;
            StatusCode = statusCode;
        }
    }

}
