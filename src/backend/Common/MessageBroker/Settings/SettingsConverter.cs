using MessageBroker.Settings.Configs;
using MessageBroker.Settings.Entities;

namespace MessageBroker.Settings;

public static class SettingsConverter
{
    public static RabbitMqConnectionSettings Convert(RabbitMqConnectionSettingsConfigEntity config)
    {
        return new RabbitMqConnectionSettings()
        {
            Port = config.Port,
            HostName = config.HostName,
            QueueName = config.QueueName
        };
    }
}