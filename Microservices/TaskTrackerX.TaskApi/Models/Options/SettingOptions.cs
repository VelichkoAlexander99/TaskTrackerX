namespace TaskTrackerX.TaskApi.Models.Options
{
    public class SettingOptions
    {
        public MicroserviceConfig AuthApi { get; set; } = default!;
        public JwtSettings JwtSettings { get; set; } = default!;
        public PaginationSettings PaginationSettings { get; set; } = default!;
    }
}
