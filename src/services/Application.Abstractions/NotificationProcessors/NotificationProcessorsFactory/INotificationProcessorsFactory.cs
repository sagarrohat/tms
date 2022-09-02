using Domain;

namespace Application;

public interface INotificationProcessorsFactory
{
    INotificationProcessor Create(EntityType entityType);
}