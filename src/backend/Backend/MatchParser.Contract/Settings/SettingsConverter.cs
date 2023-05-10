using MatchParser.Contract.Settings.Config;
using MatchParser.Contract.Settings.Entities;

namespace MatchParser.Contract.Settings;

public static class SettingsConverter
{
    public static BatchSettings Convert(BatchSettingsConfigEntity configEntity)
    {
        return new BatchSettings()
        {
            StepSize = configEntity.StepSize,
            StartId = configEntity.StartId,
            BufferSize = configEntity.BufferSize,
            RetryInterval = TimeSpan.FromSeconds(configEntity.RetryIntervalInSec)
        };
    }

    public static MatchSettings Convert(MatchSettingsConfigEntity configEntity)
    {
        return new MatchSettings()
        {
            AllowedGameModes = configEntity.AllowedGameModes.ToHashSet(),
            AllowedLobbyTypes = configEntity.AllowedLobbyTypes.ToHashSet(),
            MaxDuration = configEntity.MaxDuration,
            MaxNumberOfDireBans = configEntity.MaxNumberOfDireBans,
            MaxNumberOfRadiantBans = configEntity.MaxNumberOfRadiantBans,
            MinDuration = configEntity.MinDuration,
            NumberOfDirePicks = configEntity.NumberOfDirePicks,
            NumberOfHumanPlayers = configEntity.NumberOfHumanPlayers,
            NumberOfRadiantPicks = configEntity.NumberOfRadiantPicks,
        };
    }
}