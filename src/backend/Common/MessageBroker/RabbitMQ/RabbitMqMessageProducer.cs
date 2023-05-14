using System.Text;
using MessageBroker.Common;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace MessageBroker.RabbitMQ;

public class RabbitMqMessageProducer<T> : IMessageProducer<T>
{
    private readonly ILogger<IMessageProducer<T>> _logger;

    public RabbitMqMessageProducer(ILogger<IMessageProducer<T>> logger)
    {
        _logger = logger;
    }

    public Task SendAsync(T message)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "hello",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        var body = Encoding.UTF8.GetBytes(message.ToString());

        channel.BasicPublish(exchange: string.Empty,
            routingKey: "hello",
            basicProperties: null,
            body: body);
        _logger.LogInformation($" [x] Sent {message}");

        _logger.LogInformation(" Press [enter] to exit.");
        return Task.CompletedTask;
    }
}