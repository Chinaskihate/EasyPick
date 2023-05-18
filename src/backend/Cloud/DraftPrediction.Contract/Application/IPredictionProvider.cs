using DraftPrediction.Contract.Models;

namespace DraftPrediction.Contract.Application;

public interface IPredictionProvider
{
    Task<Prediction> GetPredictionAsync(Guid requestId, CancellationToken ct);
}