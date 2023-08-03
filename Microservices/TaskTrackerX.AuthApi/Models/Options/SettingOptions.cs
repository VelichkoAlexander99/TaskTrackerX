namespace TaskTrackerX.AuthApi.Models.Options
{
    public class SettingOptions
    {
        public RabbitMqConfig RabbitMqConfig { get; set; } = default!;
        public JwtSettings JwtSettings { get; set; } = default!;
        public PaginationSettings PaginationSettings { get; set; } = default!;
    }
}
