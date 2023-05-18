namespace MessageBroker.Settings.Configs;

public class RabbitMqConnectionSettingsConfigEntity
{
    public string HostName { get; set; }

    public int Port { get; set; }

    public string QueueName { get; set; }
}