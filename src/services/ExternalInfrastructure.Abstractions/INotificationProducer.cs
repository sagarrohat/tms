using Domain;

namespace ExternalInfrastructure;

public interface INotificationProducer
{
    void Produce(NotificationMessage notificationMessage);
}