namespace TaskTrackerX.TaskApi.Models
{
    public class Exercise
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public DateTime ReceivedDate { get; set; }
        public DateTime Deadline { get; set; }

        public Guid ExerciseStatusId { get; set; }
        public virtual Status? ExerciseStatus { get; set; }

        public Guid CreatedByUserId { get; set; }
        public Guid AssignedToUserId { get; set; }

        public bool IsArchival { get; set; } = false;
    }
}
