using Common;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace ExternalInfrastructure;

public abstract class RabbitMqBase : IDisposable
{
    protected readonly string QueueName;
    private readonly IConnection _connection;
    protected readonly IModel Channel;

    protected RabbitMqBase(IOptions<AppOptions> options)
    {
        var appOptions = options.Value;

        QueueName = appOptions.QueueOptions.NotificationQueueId;

        var connectionFactory = new ConnectionFactory
        {
            UserName = appOptions.QueueOptions.UserName,
            Password = appOptions.QueueOptions.Password,
            HostName = appOptions.QueueOptions.Host
        };

        _connection = connectionFactory.CreateConnection();

        // Channel allows us to interact with the RabbitMQ APIs
        Channel = _connection.CreateModel();

        // This will create a queue on the server if it does’t already exist.
        Channel.QueueDeclare(QueueName, durable: true, exclusive: false, autoDelete: false);
    }

    public void Dispose()
    {
        _connection.Dispose();
        Channel.Dispose();
    }
}