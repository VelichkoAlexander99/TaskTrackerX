namespace TaskTrackerX.TaskApi.DTOs.Incoming
{
    public class ExerciseCreateDto
    {
        public string Description { get; set; } = default!;
        public string Subject { get; set; } = default!;
        public DateTime ReceivedDate { get; set; } = default!;
        public DateTime Deadline { get; set; } = default!;
        public Guid ExerciseStatusId { get; set; } = default!;
        public Guid AssignedToUserId { get; set; } = default!;
    }
}
