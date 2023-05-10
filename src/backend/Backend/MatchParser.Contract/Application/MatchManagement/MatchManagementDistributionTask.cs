using Common.Data;
using Common.Tasks;
using MatchParser.Contract.Application.MatchResultProvider;
using MatchParser.Contract.Application.MatchStampManagement;
using MatchParser.Contract.Models;
using MatchParser.Contract.Settings.Entities;
using MatchParser.Contract.Validators;
using Microsoft.Extensions.Logging;

namespace MatchParser.Contract.Application.MatchManagement;

public class MatchManagementDistributionTask<TResult> : IDistributionTask
    where TResult : MatchResult
{
    private readonly IMatchResultProvider<TResult> _matchResultProvider;
    private readonly IDataManager<TResult> _dataManager;
    private readonly IMatchStampProvider _matchStampProvider;
    private readonly IMatchStampManager _matchStampManager;
    private readonly ILogger _logger;
    private readonly IMatchResultChecker<TResult> _matchValidator;
    private readonly BatchSettings _settings;

    public MatchManagementDistributionTask(
        IMatchResultProvider<TResult> matchResultProvider,
        IDataManager<TResult> dataManager,
        IMatchStampProvider matchStampProvider,
        IMatchStampManager matchStampManager,
        IMatchResultChecker<TResult> matchValidator,
        BatchSettings settings,
        ILogger logger)
    {
        _matchResultProvider = matchResultProvider;
        _dataManager = dataManager;
        _matchStampProvider = matchStampProvider;
        _matchStampManager = matchStampManager;
        _logger = logger;
        _matchValidator = matchValidator;
        _settings = settings;
    }

    public async Task RunAsync(CancellationToken ct)
    {
        var parsedLatestStamp = await _matchStampProvider.GetLatestParsedMatchStampAsync(ct);
        var maxId = parsedLatestStamp.Id + _settings.StepSize;
        var bufferedMatches = new List<TResult>(_settings.BufferSize);
        while (true)
        {
            ct.ThrowIfCancellationRequested();
            // TODO: handle only TooManyRequests
            try
            {
                var matchResultStamps = await GetMatchResultStampLowerThanAsync(maxId, ct);
                for (var i = matchResultStamps.Length - 1; i >= 0; i--)
                {
                    var currentStamp = matchResultStamps[i].Id;
                    _logger.LogInformation($"left: {i}, getting: {currentStamp}");
                    var matchResult = await _matchResultProvider.GetMatchResultAsync(currentStamp, ct);
                    if (!_matchValidator.Check(matchResult))
                    {
                        continue;
                    }

                    bufferedMatches.Add(matchResult);
                    if (bufferedMatches.Count < _settings.BufferSize)
                    {
                        _logger.LogCritical($"Buffered matches count: {bufferedMatches.Count}");
                        continue;
                    }

                    await _dataManager.SaveAsync(bufferedMatches, ct);
                    bufferedMatches.Clear();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await Task.Delay(_settings.RetryInterval, ct);
            }
            _logger.LogInformation($"Finished processing matches less than {maxId}, batch size {_settings}");
            maxId += _settings.StepSize;
        }
    }

    private async Task<MatchStamp[]> GetMatchResultStampLowerThanAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation(
            $"Getting parsed matches less than {id}, batch size {_settings}");

        var matchResultStamps = (await _matchStampProvider
            .GetMatchResultStampLowerThanAsync(id, ct)).ToArray();
        await _matchStampManager.CreateAsync(new MatchStamp()
        {
            Id = matchResultStamps.Max(s => s.Id)
        }, ct);
        return matchResultStamps;
    }
}