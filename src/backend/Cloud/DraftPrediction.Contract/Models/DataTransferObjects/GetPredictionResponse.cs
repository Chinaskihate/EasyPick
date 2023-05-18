using DraftPrediction.Contract.Models.DataTransferObjects.Drafts;

namespace DraftPrediction.Contract.Models.DataTransferObjects;

public class GetPredictionResponse
{
    public List<DraftDto> RadiantPicks { get; set; }

    public List<DraftDto> DirePicks { get; set; }

    public List<RecommendedDraftDto> RecommendedRadiantPicks { get; set; }

    public List<RecommendedDraftDto> RecommendedDirePicks { get; set; }

    public bool IsFinished { get; set; }

    public List<DraftDto> Bans { get; set; }

    public string RecommendedPosition { get; set; }
}