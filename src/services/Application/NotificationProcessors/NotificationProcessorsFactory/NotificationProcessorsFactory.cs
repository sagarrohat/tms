using Domain;
using ExternalInfrastructure;
using Persistence;

namespace Application;

public class NotificationProcessorsFactory : INotificationProcessorsFactory
{
    private readonly INotificationService _notificationService;
    private readonly IRepositoryFactory _repositoryFactory;

    public NotificationProcessorsFactory(INotificationService notificationService, IRepositoryFactory repositoryFactory)
    {
        _notificationService = notificationService;
        _repositoryFactory = repositoryFactory;
    }

    public INotificationProcessor Create(EntityType entityType)
    {
        return entityType switch
        {
            EntityType.Task => new TaskNotificationProcessor(_notificationService, _repositoryFactory),
            _ => throw new ArgumentOutOfRangeException(nameof(entityType), entityType, null)
        };
    }
}