using DraftPrediction.Settings.Configs;
using DraftPrediction.Settings.Entities;
using MessageBroker.Settings;

namespace DraftPrediction.Settings;

public class DraftPredictionServiceSettingsReader
{
    public static DraftPredictionServiceSettings ReadSettings(IConfiguration configuration)
    {
        var configEntity = configuration.Get<DraftPredictionServiceSettingsConfigEntity>();
        return new DraftPredictionServiceSettings()
        {
            PredictionProducer = SettingsConverter.Convert(configEntity.PredictionProducer),
            PredictionConsumer = SettingsConverter.Convert(configEntity.PredictionConsumer),
            CorsPolicyName = configEntity.CorsPolicyName
        };
    }

}