using AutoMapper;
using Common.Tasks;
using DraftPrediction.Contract.Models.DataTransferObjects;
using DraftPrediction.Contract.Storage;
using MessageBroker.Common;
using Microsoft.Extensions.Logging;

namespace DraftPrediction.Contract.Application.Processing;

public class PredictionProcessingDistributionTask : IDistributionTask
{
    private readonly IMessageConsumer<RecommendedDraftDto> _consumer;
    private readonly IDraftPredictionStorage _storage;
    private readonly IMapper _mapper;
    private readonly ILogger<IDistributionTask> _logger;

    public PredictionProcessingDistributionTask(
        IMessageConsumer<RecommendedDraftDto> consumer,
        IDraftPredictionStorage storage,
        IMapper mapper,
        ILogger<IDistributionTask> logger)
    {
        _consumer = consumer;
        _storage = storage;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task RunAsync(CancellationToken ct)
    {
        await _consumer.ExecuteAsync((dto) =>
        {
            _logger.LogInformation($"Started to process {dto.RequestId} prediction");
            return _storage.UpdateAsync(dto, ct);
        }, ct);
    }
}