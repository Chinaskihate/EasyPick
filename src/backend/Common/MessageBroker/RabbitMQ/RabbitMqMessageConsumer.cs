using MessageBroker.Common;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using MessageBroker.Settings.Entities;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;

namespace MessageBroker.RabbitMQ;

public class RabbitMqMessageConsumer<T> : IMessageConsumer<T>
{
    private readonly RabbitMqConnectionSettings _settings;
    private readonly ILogger<IMessageConsumer<T>> _logger;

    public RabbitMqMessageConsumer(
        RabbitMqConnectionSettings settings,
        ILogger<IMessageConsumer<T>> logger)
    {
        _settings = settings;
        _logger = logger;
    }

    public Task ExecuteAsync(Func<T, Task> func, CancellationToken ct)
    {
        var factory = new ConnectionFactory { HostName = _settings.HostName, Port = _settings.Port};
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: _settings.QueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var data = Encoding.UTF8.GetString(body);
            var entity = JsonSerializer.Deserialize<T>(data);
            func(entity).Wait(ct);
            _logger.LogInformation($" [x] Received {data}");
        };
        channel.BasicConsume(queue: _settings.QueueName,
            autoAck: true,
            consumer: consumer);

        return Task.CompletedTask;
    }
}