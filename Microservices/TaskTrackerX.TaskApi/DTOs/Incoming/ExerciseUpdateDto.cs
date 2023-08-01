namespace TaskTrackerX.TaskApi.DTOs.Incoming
{
    public class ExerciseUpdateDto
    {
        public string? Description { get; set; }
        public string? Subject { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime? Deadline { get; set; }
        public Guid? ExerciseStatusId { get; set; }
    }
}
