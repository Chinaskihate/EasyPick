using MatchParser.Contract.Models;

namespace MatchParser.Contract.Settings.Entities;

public class MatchSettings
{
    public int MinDuration { get; set; }

    public int MaxDuration { get; set; }

    public int NumberOfRadiantPicks { get; set; }

    public int NumberOfDirePicks { get; set; }

    public int MaxNumberOfRadiantBans { get; set; }

    public int MaxNumberOfDireBans { get; set; }

    public int NumberOfHumanPlayers { get; set; }

    public ISet<LobbyType> AllowedLobbyTypes { get; set; }

    public ISet<GameMode> AllowedGameModes { get; set; }
}