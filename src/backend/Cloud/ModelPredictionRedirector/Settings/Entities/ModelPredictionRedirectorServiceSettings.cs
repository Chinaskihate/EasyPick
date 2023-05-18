using MessageBroker.Settings.Entities;

namespace ModelPredictionRedirector.Settings.Entities;

public class ModelPredictionRedirectorServiceSettings
{
    public RabbitMqConnectionSettings RedirectorProducer { get; set; }

    public RabbitMqConnectionSettings RedirectorConsumer { get; set; }
}