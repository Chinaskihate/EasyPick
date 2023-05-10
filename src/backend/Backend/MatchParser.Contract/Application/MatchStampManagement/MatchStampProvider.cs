using AutoMapper;
using Http;
using MatchParser.Contract.Models;
using MatchParser.Contract.Models.DataTransferObjects;
using MatchParser.Contract.Settings.Entities;
using MatchParser.Contract.Storage;
using Microsoft.EntityFrameworkCore;

namespace MatchParser.Contract.Application.MatchStampManagement;

public class MatchStampProvider : IMatchStampProvider
{
    private readonly IHttpClientFactory<GenericHttpClient> _httpClientFactory;
    private readonly string _httpClientName;
    private readonly IDbContextFactory<MatchParserDbContext> _dbContextFactory;
    private readonly long _startId;
    private readonly IMapper _mapper;

    public MatchStampProvider(
        IHttpClientFactory<GenericHttpClient> httpClientFactory,
        string httpClientName,
        IDbContextFactory<MatchParserDbContext> dbContextFactory,
        long startId,
        IMapper mapper)
    {
        _httpClientFactory = httpClientFactory;
        _httpClientName = httpClientName;
        _dbContextFactory = dbContextFactory;
        _startId = startId;
        _mapper = mapper;
    }

    public async Task<MatchStamp> GetLatestParsedMatchStampAsync(CancellationToken ct)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(ct);
        var count = await context.MatchStamps.CountAsync(ct);
        if (count == 0)
        {
            return new MatchStamp() { Id = _startId };
        }
        var maxId = await context.MatchStamps
            .MaxAsync(s => s.MatchId, cancellationToken: ct);
        var dto = await context.MatchStamps
            .Where(s => s.MatchId == maxId)
            .SingleAsync(ct);
        return _mapper.Map<MatchStamp>(dto);
    }

    public async Task<IEnumerable<MatchStamp>> GetMatchResultStampLowerThanAsync(long matchId, CancellationToken ct)
    {
        using var httpClient = _httpClientFactory.CreateClient(_httpClientName);
        var dtos = await httpClient
            .GetFromJsonAsync<MatchStampDto[]>(Routes.GetMatchesLessThanId + matchId, ct: ct);
        return _mapper.Map<MatchStampDto[], MatchStamp[]>(dtos);
    }
}