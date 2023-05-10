using MatchParser.Contract.Models;

namespace MatchParser.Contract.Application.MatchResultProvider;

public interface IMatchResultProvider<T> where T : MatchResult
{
    Task<T> GetMatchResultAsync(long matchId, CancellationToken ct);
}