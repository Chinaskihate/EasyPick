using MatchParser.Contract.Models;

namespace MatchParser.Contract.Validators;

public interface IMatchResultChecker<in T>
    where T : MatchResult
{
    public bool Check(T matchResult);
}