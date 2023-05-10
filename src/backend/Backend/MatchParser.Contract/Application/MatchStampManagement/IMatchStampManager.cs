using MatchParser.Contract.Models;

namespace MatchParser.Contract.Application.MatchStampManagement;

public interface IMatchStampManager
{
    Task CreateAsync(MatchStamp matchStamp, CancellationToken ct);
}