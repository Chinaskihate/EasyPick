namespace DraftPrediction.Contract.Application.Prediction;

public class PredictionRequest
{
    public List<HeroInfo> RadiantPicks { get; set; } = null!;
    public List<HeroInfo> RadiantBans { get; set; } = null!;
    public List<HeroInfo> DirePicks { get; set; } = null!;
    public List<HeroInfo> DireBans { get; set; } = null!;
}