namespace DraftPrediction.Contract.Application.Drafts;

public class DraftInfo
{
    public HeroInfo Hero { get; set; } = null!;

    public double Rate { get; set; }
}