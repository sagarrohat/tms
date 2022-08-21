using Domain;
using Task = System.Threading.Tasks.Task;

namespace Persistence;

public interface INotificationRepository
{
    Task CreateAsync(Notification notification);
}

