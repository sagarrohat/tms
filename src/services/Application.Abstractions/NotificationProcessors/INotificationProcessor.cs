using Domain;
using Task = System.Threading.Tasks.Task;

namespace Application;

public interface INotificationProcessor
{
    Task ProcessAsync(NotificationMessage notificationMessage);
}