namespace MessageBroker.Settings.Entities;

public class RabbitMqConnectionSettings
{
    public string HostName { get; set; }

    public int Port { get; set; }

    public string QueueName { get; set; }
}