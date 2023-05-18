using MessageBroker.Settings.Configs;

namespace ModelPredictionRedirector.Settings.Configs;

public class ModelPredictionRedirectorServiceSettingConfigEntity
{
    public RabbitMqConnectionSettingsConfigEntity RedirectorProducer { get; set; }

    public RabbitMqConnectionSettingsConfigEntity RedirectorConsumer { get; set; }
}