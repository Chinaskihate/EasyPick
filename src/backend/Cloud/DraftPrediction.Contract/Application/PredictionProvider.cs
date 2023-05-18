using DraftPrediction.Contract.Models;
using DraftPrediction.Contract.Storage;

namespace DraftPrediction.Contract.Application;

public class PredictionProvider : IPredictionProvider
{
    private readonly IDraftPredictionStorage _storage;

    public PredictionProvider(IDraftPredictionStorage storage)
    {
        _storage = storage;
    }

    public async Task<Prediction> GetPredictionAsync(Guid requestId, CancellationToken ct)
    {
        return await _storage.GetAsync(requestId, ct);
    }
}