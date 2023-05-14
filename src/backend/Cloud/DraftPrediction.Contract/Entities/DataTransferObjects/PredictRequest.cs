using DraftPrediction.Contract.Entities.DataTransferObjects.Drafts;

namespace DraftPrediction.Contract.Entities.DataTransferObjects;

public class PredictRequest
{
    public List<PickDto> RadiantPicks { get; set; }

    public List<PickDto> DirePicks { get; set; }

    public List<BanDto> Bans { get; set; }
}