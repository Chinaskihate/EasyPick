using DraftPrediction.Contract.Models.DataTransferObjects.Drafts;

namespace DraftPrediction.Contract.Models.DataTransferObjects;

public class PredictRequest
{
    public List<DraftDto> RadiantPicks { get; set; }

    public List<DraftDto> DirePicks { get; set; }

    public List<DraftDto> Bans { get; set; }

    public string RecommendedPosition { get; set; }
}