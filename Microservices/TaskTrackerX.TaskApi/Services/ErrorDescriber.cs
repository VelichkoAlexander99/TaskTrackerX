using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Resources;
using TaskTrackerX.TaskApi.Models;

namespace TaskTrackerX.TaskApi.Services
{
    public class ErrorDescriber
    {
        public static ErrorInfo DefaultError()
        {
            return new ErrorInfo
            {
                Code = nameof(DefaultError),
                Description = "Access denied"
            };
        }

        public static ErrorInfo ServerNotResponding(string name)
        {
            return new ErrorInfo
            {
                Code = nameof(ServerNotResponding),
                Description = $"Server {name} not responding"
            };
        }

        public static ErrorInfo ConcurrencyFailure()
        {
            return new ErrorInfo
            {
                Code = nameof(ConcurrencyFailure),
                Description = "An error occurred while updating the database"
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

        public static ErrorInfo InvalidStatus()
        {
            return new ErrorInfo
            {
                Code = nameof(InvalidStatus),
                Description = $"Status is not found"
            };
        }

        public static ErrorInfo InvalidExercise()
        {
            return new ErrorInfo
            {
                Code = nameof(InvalidExercise),
                Description = $"Exercise is not found"
            };
        }

        public static ErrorInfo InvalidStatusName(string? role)
        {
            return new ErrorInfo
            {
                Code = nameof(InvalidStatusName),
                Description = $"Status name invalid : {role}"
            };
        }

        public static ErrorInfo DuplicateRoleName(string name)
        {
            return new ErrorInfo
            {
                Code = nameof(DuplicateRoleName),
                Description = $"Status {name} already exists"
            };
        }

        public static ErrorInfo InvalidExerciseDescription()
        {
            return new ErrorInfo
            {
                Code = nameof(InvalidExerciseDescription),
                Description = $"Exercise description not entered"
            };
        }

        public static ErrorInfo InvalidExerciseDescriptionMinLength(int minLength, int currentLength)
        {
            return new ErrorInfo
            {
                Code = nameof(InvalidExerciseDescriptionMinLength),
                Description = $"The description must contain at least {minLength} characters. Current {currentLength}"
            };
        }

        public static ErrorInfo InvalidExerciseDescriptionMaxLength(int maxLength, int currentLength)
        {
            return new ErrorInfo
            {
                Code = nameof(InvalidExerciseDescriptionMaxLength),
                Description = $"The description must contain a maximum of {maxLength} characters. Current {currentLength}"
            };
        }

        public static ErrorInfo InvalidExerciseReceivedDateAndDeadline(DateTime startDate, DateTime endDate)
        {
            return new ErrorInfo
            {
                Code = nameof(InvalidExerciseReceivedDateAndDeadline),
                Description = $"The start time {startDate} must not be greater than or equal to the end time {endDate}."
            };
        }

        public static ErrorInfo InvalidExerciseSubject()
        {
            return new ErrorInfo
            {
                Code = nameof(InvalidExerciseSubject),
                Description = $"Exercise subject not entered"
            };
        }

        public static ErrorInfo InvalidExerciseSubjectMinLength(int minLength, int currentLength)
        {
            return new ErrorInfo
            {
                Code = nameof(InvalidExerciseSubjectMinLength),
                Description = $"The subject must contain at least {minLength} characters. Current {currentLength}"
            };
        }

        public static ErrorInfo InvalidExerciseSubjectMaxLength(int maxLength, int currentLength)
        {
            return new ErrorInfo
            {
                Code = nameof(InvalidExerciseSubjectMaxLength),
                Description = $"The subject must contain a maximum of {maxLength} characters. Current {currentLength}"
            };
        }
    }
}
