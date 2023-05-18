using AutoMapper;
using DraftPrediction.Contract.Models;
using DraftPrediction.Contract.Models.DataTransferObjects;
using DraftPrediction.Contract.Models.DataTransferObjects.Drafts;
using DraftPrediction.Contract.Models.Drafts;

namespace DraftPrediction.Contract.Storage;

public class DraftPredictionStorage : IDraftPredictionStorage
{
    private readonly IMapper _mapper;
    private readonly IDictionary<Guid, Prediction> _predictions;

    public DraftPredictionStorage(IMapper mapper)
    {
        _mapper = mapper;
        _predictions = new Dictionary<Guid, Prediction>();
    }

    public Task<Prediction> AddAsync(PredictInfo info, CancellationToken ct)
    {
        var id = GenerateGuid();
        _predictions.Add(id, GetPrediction(id, info));
        return Task.FromResult(_predictions[id]);
    }

    public Task UpdateAsync(RecommendationsDto dto, CancellationToken ct)
    {
        var prediction = _predictions[dto.RequestId];
        prediction.RecommendedRadiantPicks = _mapper.Map<List<RecommendedDraftDto>, List<RecommendedDraft>>(dto.RecommendedRadiantPicks);
        prediction.RecommendedDirePicks = _mapper.Map<List<RecommendedDraftDto>, List<RecommendedDraft>>(dto.RecommendedDirePicks);
        prediction.IsFinished = true;
        return Task.CompletedTask;
    }

    public Task<Prediction> GetAsync(Guid id, CancellationToken ct)
    {
        if (!_predictions.ContainsKey(id))
        {
            throw new KeyNotFoundException($"No {id} in draft prediction storage");
        }
        
        return Task.FromResult(_predictions[id]);
    }

    private Prediction GetPrediction(Guid id, PredictInfo info)
    {
        return new Prediction()
        {
            Id = id,
            Bans = _mapper.Map<List<Draft>>(info.Bans),
            DirePicks = _mapper.Map<List<Draft>>(info.DirePicks),
            RadiantPicks = _mapper.Map<List<Draft>>(info.RadiantPicks),
            RecommendedPosition = info.RecommendedPosition
        };
    }

    private Guid GenerateGuid()
    {
        while (_predictions.ContainsKey(Guid.NewGuid())) { }
        return Guid.NewGuid();
    }
}