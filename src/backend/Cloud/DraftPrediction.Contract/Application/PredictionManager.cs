using AutoMapper;
using DraftPrediction.Contract.Models;
using DraftPrediction.Contract.Models.DataTransferObjects;
using DraftPrediction.Contract.Storage;
using MessageBroker.Common;

namespace DraftPrediction.Contract.Application;

public class PredictionManager : IPredictionManager
{
    private readonly IDraftPredictionStorage _storage;
    private readonly IMessageProducer<PredictDraftDto> _producer;
    private readonly IMapper _mapper;

    public PredictionManager(
        IDraftPredictionStorage storage,
        IMessageProducer<PredictDraftDto> producer,
        IMapper mapper)
    {
        _storage = storage;
        _producer = producer;
        _mapper = mapper;
    }

    public async Task<Guid> Predict(PredictInfo request, CancellationToken ct)
    {
        var prediction = await _storage.AddAsync(request, ct);
        var dto = _mapper.Map<Prediction, PredictDraftDto>(prediction);
        await _producer.SendAsync(dto);
        return prediction.Id;
    }
}