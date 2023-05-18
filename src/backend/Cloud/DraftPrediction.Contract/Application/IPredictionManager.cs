using DraftPrediction.Contract.Models.DataTransferObjects;

namespace DraftPrediction.Contract.Application;

public interface IPredictionManager
{
    Task<Guid> Predict(PredictInfo request, CancellationToken ct);
}