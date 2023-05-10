namespace MatchParser.Contract.Models;

public class MatchResult
{
    public long Id { get; set; }

    public bool RadiantWin { get; set; }

    public int Duration { get; set; }

    public GameMode GameMode { get; set; }

    public LobbyType LobbyType { get; set; }

    public List<PickBan> PickBans { get; set; }

    public int DireScore { get; set; }

    public int RadiantScore { get; set; }

    public int HumanPlayersCount { get; set; }
}