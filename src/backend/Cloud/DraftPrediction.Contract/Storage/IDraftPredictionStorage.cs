using DraftPrediction.Contract.Models;
using DraftPrediction.Contract.Models.DataTransferObjects;

namespace DraftPrediction.Contract.Storage;

public interface IDraftPredictionStorage
{
    Task<Prediction> AddAsync(PredictInfo info, CancellationToken ct);

    Task UpdateAsync(RecommendationsDto dto, CancellationToken ct);

    Task<Prediction> GetAsync(Guid id, CancellationToken ct);
}