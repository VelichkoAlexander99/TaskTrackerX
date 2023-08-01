using Microsoft.AspNetCore.Identity;

namespace TaskTrackerX.TaskApi.Models
{
    public class Result
    {
        private static readonly Result _success = new Result { Succeeded = true };
        private readonly List<ErrorInfo> _errors = new List<ErrorInfo>();

        public bool Succeeded { get; protected set; }

        public IEnumerable<ErrorInfo> Errors => _errors;

        public static Result Success => _success;

        public static Result Failed(params ErrorInfo[] errors)
        {
            var result = new Result { Succeeded = false };
            if (errors != null)
            {
                result._errors.AddRange(errors);
            }
            return result;
        }

    }
}
