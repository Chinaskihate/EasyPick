namespace MatchParser.Contract.Models;

public class PickBan
{
    public bool IsPick { get; set; }

    public int HeroId { get; set; }

    public TeamSide Team { get; set; }
}