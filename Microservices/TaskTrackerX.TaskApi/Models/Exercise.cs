namespace TaskTrackerX.TaskApi.Models
{
    public class Exercise
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        public DateTime ReceivedDate { get; set; }
        public DateTime Deadline { get; set; }

        public Guid ExerciseStatusId { get; set; }
        public virtual Status? ExerciseStatus { get; set; }

        public Guid CreatedByUserId { get; set; }
        public Guid AssignedToUserId { get; set; }
    }
}
