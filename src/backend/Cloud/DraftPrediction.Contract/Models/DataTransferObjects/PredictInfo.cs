﻿using DraftPrediction.Contract.Models.DataTransferObjects.Drafts;

namespace DraftPrediction.Contract.Models.DataTransferObjects;

public class PredictInfo
{
    public List<DraftDto> RadiantPicks { get; set; }

    public List<DraftDto> DirePicks { get; set; }

    public List<DraftDto> Bans { get; set; }

    public Position RecommendedPosition { get; set; }
}