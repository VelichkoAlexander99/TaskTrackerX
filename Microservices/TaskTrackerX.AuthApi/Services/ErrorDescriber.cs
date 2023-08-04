using TaskTrackerX.AuthApi.Models;

namespace TaskTrackerX.AuthApi.Services
{
    public static class ErrorDescriber
    {
        public static ErrorInfo DefaultError()
        {
            return new ErrorInfo
            {
                Code = nameof(DefaultError),
                Description = "Access denied"
            };
        }

        public static ErrorInfo InvalidUserArchival(string? userName)
        {
            return new ErrorInfo
            {
                Code = nameof(InvalidUserArchival),
                Description = string.IsNullOrEmpty(userName) ?
                    $"This user is disabled, you cannot interact with him" :
                    $"This user {userName} is disabled, you cannot interact with him"
            };
        }

        public static ErrorInfo InvalidUser()
        {
            return new ErrorInfo
            {
                Code = nameof(InvalidUser),
                Description = $"User is not found"
            };
        }

        public static ErrorInfo InvalidRole()
        {
            return new ErrorInfo
            {
                Code = nameof(InvalidRole),
                Description = $"RoleName is not found"
            };
        }

        public static ErrorInfo InvalidUserLoginOrPassword()
        {
            return new ErrorInfo
            {
                Code = nameof(InvalidUserLoginOrPassword),
                Description = $"Incorrect login or password entered"
            };
        }
    }
}
