using DraftPrediction.Contract.Models.Drafts;

namespace DraftPrediction.Contract.Models;

public class Prediction
{
    public Guid Id { get; set; }

    public List<Draft> RadiantPicks { get; set; }

    public List<Draft> DirePicks { get; set; }

    public List<Draft> Bans { get; set; }

    public List<RecommendedDraft> RecommendedRadiantPicks { get; set; }

    public List<RecommendedDraft> RecommendedDirePicks { get; set; }

    public bool IsFinished { get; set; }

    public Position RecommendedPosition { get; set; }
}