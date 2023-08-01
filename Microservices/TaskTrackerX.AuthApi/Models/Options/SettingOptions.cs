namespace TaskTrackerX.AuthApi.Models.Options
{
    public class SettingOptions
    {
        public JwtSettings JwtSettings { get; set; } = default!;
        public PaginationSettings PaginationSettings { get; set; } = default!;
    }
}
