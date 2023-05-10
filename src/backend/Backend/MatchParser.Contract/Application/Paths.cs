namespace MatchParser.Contract.Application;

// TODO: move to appsettings, find a way to pass query params
public static class Routes
{
    public const string GetMatchById = $"matches/";

    public const string GetMatchesLessThanId = $"parsedMatches?less_than_match_id=";
}