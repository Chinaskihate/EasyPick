using MessageBroker.Settings.Entities;

namespace DraftPrediction.Settings.Entities;

public class DraftPredictionServiceSettings
{
    public RabbitMqConnectionSettings PredictionProducer { get; set; }

    public RabbitMqConnectionSettings PredictionConsumer { get; set; }

    public string CorsPolicyName { get; set; }
}