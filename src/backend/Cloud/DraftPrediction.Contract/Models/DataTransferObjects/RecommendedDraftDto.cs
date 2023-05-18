using DraftPrediction.Contract.Models.DataTransferObjects.Drafts;

namespace DraftPrediction.Contract.Models.DataTransferObjects;

public class RecommendationsDto
{
    public Guid RequestId { get; set; }

    public List<RecommendedDraftDto> RecommendedRadiantPicks { get; set; }

    public List<RecommendedDraftDto> RecommendedDirePicks { get; set; }
}