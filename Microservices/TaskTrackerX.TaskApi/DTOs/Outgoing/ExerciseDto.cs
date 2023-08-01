namespace TaskTrackerX.TaskApi.DTOs.Outgoing
{
    public class ExerciseDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        public DateTime ReceivedDate { get; set; }
        public DateTime Deadline { get; set; }

        public Guid ExerciseStatusId { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid AssignedToUserId { get; set; }
    }
}
