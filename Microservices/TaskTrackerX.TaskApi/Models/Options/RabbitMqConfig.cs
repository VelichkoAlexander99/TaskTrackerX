namespace TaskTrackerX.TaskApi.Models.Options
{
    public class RabbitMqConfig
    {
        public string ConnectionString { get; set; } = default!;
        public string QueueAuthUserDelete { get; set; } = default!;
    }
}
