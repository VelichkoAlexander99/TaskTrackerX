using System.Net;
using System.Text.Json.Serialization;

namespace TaskTrackerX.AuthApi.Models
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public bool HasErrors => ErrorMessage.Any();
        public IEnumerable<ErrorInfo> ErrorMessage { get; set; } = new List<ErrorInfo>();
    }
}
