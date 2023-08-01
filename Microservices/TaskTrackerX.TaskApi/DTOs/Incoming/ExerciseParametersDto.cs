namespace TaskTrackerX.TaskApi.DTOs.Incoming
{
    public class ExerciseParametersDto : QueryParametersDto
    {
        public Guid? ExerciseStatusId { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public Guid? AssignedToUserId { get; set; }
    }
}
