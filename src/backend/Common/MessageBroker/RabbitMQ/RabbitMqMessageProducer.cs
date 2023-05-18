using System.Text;
using System.Text.Json;
using MessageBroker.Common;
using MessageBroker.Settings.Entities;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace MessageBroker.RabbitMQ;

public class RabbitMqMessageProducer<T> : IMessageProducer<T>
{
    private readonly RabbitMqConnectionSettings _settings;
    private readonly ILogger<IMessageProducer<T>> _logger;

    public RabbitMqMessageProducer(
        RabbitMqConnectionSettings settings,
        ILogger<IMessageProducer<T>> logger)
    {
        _settings = settings;
        _logger = logger;
    }

    public Task SendAsync(T message)
    {
        var factory = new ConnectionFactory
        {
            HostName = _settings.HostName, 
            Port = _settings.Port
        };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: _settings.QueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        channel.BasicPublish(exchange: string.Empty,
            routingKey: _settings.QueueName,
            basicProperties: null,
            body: body);

        return Task.CompletedTask;
    }
}