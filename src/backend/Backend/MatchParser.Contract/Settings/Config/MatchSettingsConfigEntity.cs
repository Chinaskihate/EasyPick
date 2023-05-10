using MatchParser.Contract.Models;

namespace MatchParser.Contract.Settings.Config;

public class MatchSettingsConfigEntity
{
    public int MinDuration { get; set; }

    public int MaxDuration { get; set; }

    public int NumberOfRadiantPicks { get; set; }

    public int NumberOfDirePicks { get; set; }

    public int MaxNumberOfRadiantBans { get; set; }

    public int MaxNumberOfDireBans { get; set; }

    public int NumberOfHumanPlayers { get; set; }

    public IEnumerable<LobbyType> AllowedLobbyTypes { get; set; }

    public IEnumerable<GameMode> AllowedGameModes { get; set; }
}