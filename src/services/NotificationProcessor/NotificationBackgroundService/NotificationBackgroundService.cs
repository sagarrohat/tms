using Application;
using ExternalInfrastructure;
using Task = System.Threading.Tasks.Task;

namespace NotificationProcessor;

public class NotificationBackgroundService : BackgroundService, INotificationBackgroundService
{
    private readonly INotificationConsumer _notificationConsumer;
    private readonly INotificationProcessorsFactory _notificationProcessorsFactory;

    public NotificationBackgroundService(INotificationConsumer notificationConsumer,
        INotificationProcessorsFactory notificationProcessorsFactory)
    {
        _notificationConsumer = notificationConsumer;
        _notificationProcessorsFactory = notificationProcessorsFactory;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _notificationConsumer.Consume(async (result) =>
        {
            var notificationProcessor = _notificationProcessorsFactory.Create(result.EntityType);
            await notificationProcessor.ProcessAsync(result);
        });

        while (!stoppingToken.IsCancellationRequested)
        {
        }

        return Task.CompletedTask;
    }
}