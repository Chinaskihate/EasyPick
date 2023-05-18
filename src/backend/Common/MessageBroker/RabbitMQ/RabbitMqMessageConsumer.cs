using MessageBroker.Common;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using MessageBroker.Settings.Entities;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;

namespace MessageBroker.RabbitMQ;

public class RabbitMqMessageConsumer<T> : IMessageConsumer<T>, IDisposable
{
    private readonly RabbitMqConnectionSettings _settings;
    private readonly ILogger<IMessageConsumer<T>> _logger;
    private readonly IConnection _connection;

    public RabbitMqMessageConsumer(
        RabbitMqConnectionSettings settings,
        ILogger<IMessageConsumer<T>> logger)
    {
        _settings = settings;
        _logger = logger;
        var factory = new ConnectionFactory { HostName = _settings.HostName, Port = _settings.Port };
        _connection = factory.CreateConnection();
    }

    public Task ExecuteAsync(Func<T, Task> func, CancellationToken ct)
    {
        using var channel = _connection.CreateModel();

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

    public void Dispose()
    {
        _connection.Dispose();
    }
}