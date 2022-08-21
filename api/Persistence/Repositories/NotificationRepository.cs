using Domain;
using Task = System.Threading.Tasks.Task;

namespace Persistence;

public class NotificationRepository : INotificationRepository
{
    private readonly DatabaseContext _dbContext;
    
    public NotificationRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(Notification notification)
    {
        notification.Id = Guid.NewGuid();
        notification.Unread = true;
        notification.CreatedOnUtc = DateTime.UtcNow;
        
        _dbContext.Add(notification);
        await _dbContext.SaveChangesAsync();
    }
}

