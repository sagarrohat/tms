using System.Text;
using Common;
using Domain;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace ExternalInfrastructure;

public class RabbitMqNotificationProducer : RabbitMqBase, INotificationProducer
{
    private readonly IBasicProperties _basicProperties;

    public RabbitMqNotificationProducer(IOptions<AppOptions> options) : base(options)
    {
        _basicProperties = Channel.CreateBasicProperties();
        _basicProperties.Persistent = true;
    }

    public void Produce(NotificationMessage notificationMessage)
    {
        var serializedJson = JsonConvert.SerializeObject(notificationMessage);

        var bodyAsByteArray = Encoding.UTF8.GetBytes(serializedJson);

        Channel.BasicPublish(exchange: string.Empty, routingKey: QueueName, basicProperties: _basicProperties,
            body: bodyAsByteArray);
    }
}