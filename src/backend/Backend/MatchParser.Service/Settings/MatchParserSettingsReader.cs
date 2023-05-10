using MatchParser.Contract.Settings;
using MatchParser.Service.Settings.Config;
using MatchParser.Service.Settings.Entities;

namespace MatchParser.Service.Settings;

public static class MatchParserSettingsReader
{
    public static MatchParserSettings ReadSettings(IConfiguration configuration)
    {
        var configEntity = configuration.Get<MatchParserSettingsConfigEntity>();
        return new MatchParserSettings()
        {
            BatchSettings = SettingsConverter.Convert(configEntity.BatchSettings),
            MatchSettings = SettingsConverter.Convert(configEntity.MatchSettings),
            PathToDirectory = configEntity.PathToDirectory,
            MatchesDataUrl = configEntity.MatchesDataUrl,
            DbConnectionString = configEntity.DbConnectionString,
        };
    }
}