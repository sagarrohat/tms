using Domain;
using Task = System.Threading.Tasks.Task;

namespace ExternalInfrastructure;

public interface INotificationConsumer
{
    void Consume(Func<NotificationMessage, Task> consumeAction);
}