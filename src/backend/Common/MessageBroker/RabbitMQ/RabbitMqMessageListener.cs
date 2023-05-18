using MessageBroker.Common;
using MessageBroker.Settings.Entities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace MessageBroker.RabbitMQ;

public class RabbitMqMessageListener<T> : IHostedService, IMessageListener<T>
{
    private readonly RabbitMqConnectionSettings _settings;
    private readonly ILogger<IMessageListener<T>> _logger;
    private readonly Func<T, Task> _func;
    private IConnection _connection;
    private IModel _channel;
    private Timer? _timer = null;

    public RabbitMqMessageListener(
        Func<T, Task> func,
        RabbitMqConnectionSettings settings,
        ILogger<IMessageListener<T>> logger)
    {
        _func = func;
        _settings = settings;
        _logger = logger;
        var factory = new ConnectionFactory
        {
            HostName = settings.HostName,
            Port = settings.Port
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: settings.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(Listen, null, 0, 1000);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    private void Listen(object? state)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());
            var entity = JsonSerializer.Deserialize<T>(content);
            try
            {
                _func(entity).Wait(CancellationToken.None);
                _logger.LogInformation("Got message: " + content);
                _channel.BasicAck(ea.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        };

        _channel.BasicConsume(_settings.QueueName, false, consumer);
    }
}