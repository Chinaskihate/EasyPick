using MessageBroker.Settings.Configs;

namespace DraftPrediction.Settings.Configs;

public class DraftPredictionServiceSettingsConfigEntity
{
    public RabbitMqConnectionSettingsConfigEntity PredictionProducer { get; set; }

    public RabbitMqConnectionSettingsConfigEntity PredictionConsumer { get; set; }

    public string CorsPolicyName { get; set; }
}