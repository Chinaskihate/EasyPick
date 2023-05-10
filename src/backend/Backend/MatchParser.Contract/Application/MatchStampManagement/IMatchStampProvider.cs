using MatchParser.Contract.Models;

namespace MatchParser.Contract.Application.MatchStampManagement;

public interface IMatchStampProvider
{
    Task<MatchStamp> GetLatestParsedMatchStampAsync(CancellationToken ct);

    Task<IEnumerable<MatchStamp>> GetMatchResultStampLowerThanAsync(long matchId, CancellationToken ct);
}