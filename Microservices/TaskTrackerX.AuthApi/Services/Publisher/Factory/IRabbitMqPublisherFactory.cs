namespace TaskTrackerX.AuthApi.Services.Publisher.Factory
{
    public interface IRabbitMqPublisherFactory : IDisposable
    {
        void PublisherUserDelete(Guid userId);
    }
}
