using AutoMapper;
using Http;
using MatchParser.Contract.Models;
using MatchParser.Contract.Models.DataTransferObjects;

namespace MatchParser.Contract.Application.MatchResultProvider;

public class MatchResultProvider<T> : IMatchResultProvider<T>
    where T : MatchResult
{
    private readonly IHttpClientFactory<GenericHttpClient> _httpClientFactory;
    private readonly string _httpClientName;
    private readonly IMapper _mapper;

    public MatchResultProvider(
        IHttpClientFactory<GenericHttpClient> httpClientFactory,
        string httpClientName,
        IMapper mapper)
    {
        _httpClientFactory = httpClientFactory;
        _httpClientName = httpClientName;
        _mapper = mapper;
    }

    public async Task<T> GetMatchResultAsync(long matchId, CancellationToken ct = default)
    {
        using var httpClient = _httpClientFactory.CreateClient(_httpClientName);
        var dto = await httpClient
            .GetFromJsonAsync<MatchResultDto>(Routes.GetMatchById + matchId, ct: ct);
        return _mapper.Map<T>(dto);
    }
}