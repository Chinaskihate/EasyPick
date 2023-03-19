using DraftPrediction.Contract.Application.Drafts;

namespace DraftPrediction.Contract.Application.Prediction;

public class PredictionResponse
{
    public List<PickInfo> RecommendedRadiantPicks { get; set; }

    public List<BanInfo> RecommendedRadiantBans { get; set; }

    public List<PickInfo> RecommendedDirePicks { get; set; }

    public List<BanInfo> RecommendedDireBans { get; set; }
}