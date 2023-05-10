using System.Text;
using MessageBroker.Common;
using RabbitMQ.Client;

namespace MessageBroker.RabbitMQ;

public class RabbitMqMessageProducer<T> : IMessageProducer<T>
{
    public RabbitMqMessageProducer()
    {
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
        Console.WriteLine($" [x] Sent {message}");

        Console.WriteLine(" Press [enter] to exit.");
        return Task.CompletedTask;
    }
}