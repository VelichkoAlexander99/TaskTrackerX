using System.Net;
using System.Text.Json.Serialization;

namespace TaskTrackerX.AuthApi.Models
{
    public class ApiResponse<TResult>
    {
        public TResult? Data { get; protected set; }
        public IEnumerable<string> ErrorMessage { get; protected set; }

        [JsonIgnore]
        public int StatusCode { get; protected set; }

        public bool HasErrors => ErrorMessage.Any();

        protected ApiResponse(HttpStatusCode statusCode, IEnumerable<string> errors, TResult value)
        {
            StatusCode = (int)statusCode;
            Data = value;
            ErrorMessage = errors ?? new List<string>();
        }

        public static ApiResponse<TResult> Success(TResult result, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ApiResponse<TResult>(statusCode, default!, result);
        }
        public static ApiResponse<TResult> Fail(HttpStatusCode statusCode = HttpStatusCode.BadRequest, params string[] errors)
        {
            return new ApiResponse<TResult>(statusCode, errors, default!);
        }
    }
}
