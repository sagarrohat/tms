using System.Text;
using Common;
using Domain;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Task = System.Threading.Tasks.Task;

namespace ExternalInfrastructure;

public class RabbitMqNotificationConsumer : RabbitMqBase, INotificationConsumer
{
    public RabbitMqNotificationConsumer(IOptions<AppOptions> options) : base(options)
    {
        Channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
    }

    public void Consume(Func<NotificationMessage, Task> consumeAction)
    {
        var consumer = new EventingBasicConsumer(Channel);
        consumer.Received += async (model, eventArgs) =>
        {
            var bodyAsByteArray = eventArgs.Body.ToArray();
            var bodyAsString = Encoding.UTF8.GetString(bodyAsByteArray);

            await consumeAction(JsonConvert.DeserializeObject<NotificationMessage>(bodyAsString));

            Channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
        };

        Channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);
    }
}