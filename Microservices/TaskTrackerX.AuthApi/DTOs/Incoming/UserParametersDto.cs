namespace TaskTrackerX.AuthApi.DTOs.Incoming
{
    public class UserParametersDto : QueryParametersDto
    {
        public string? Name { get; set; }
        public string? Login { get; set; }
    }
}
