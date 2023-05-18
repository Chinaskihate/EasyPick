using MessageBroker.Settings;
using ModelPredictionRedirector.Settings.Configs;
using ModelPredictionRedirector.Settings.Entities;

namespace ModelPredictionRedirector.Settings;

public class ModelPredictionRedirectorServiceSettingsReader
{
    public static ModelPredictionRedirectorServiceSettings ReadSettings(IConfiguration configuration)
    {
        var configEntity = configuration.Get<ModelPredictionRedirectorServiceSettingConfigEntity>();
        return new ModelPredictionRedirectorServiceSettings()
        {
            RedirectorProducer = SettingsConverter.Convert(configEntity.RedirectorProducer),
            RedirectorConsumer = SettingsConverter.Convert(configEntity.RedirectorConsumer)
        };
    }
}