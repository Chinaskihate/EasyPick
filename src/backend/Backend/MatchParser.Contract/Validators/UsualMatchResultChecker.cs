using MatchParser.Contract.Models;
using MatchParser.Contract.Settings.Entities;
using Microsoft.Extensions.Logging;
using System.Text;

namespace MatchParser.Contract.Validators;

public class UsualMatchResultChecker<T> : IMatchResultChecker<T>
    where T : MatchResult
{
    private readonly MatchSettings _settings;
    private readonly ILogger _logger;

    public UsualMatchResultChecker(
        MatchSettings settings,
        ILogger logger)
    {
        _settings = settings;
        _logger = logger;
    }

    public bool Check(T matchResult)
    {
        return CheckDuration(matchResult) &&
            CheckPicks(matchResult) &&
            CheckBans(matchResult) &&
            CheckNumberOfHumanPlayers(matchResult) &&
            CheckLobbyType(matchResult) &&
            CheckGameMode(matchResult);
    }

    private bool CheckDuration(T matchResult)
    {
        var duration = matchResult.Duration;
        if (duration < _settings.MinDuration || duration > _settings.MaxDuration)
        {
            _logger.LogWarning(@$"Match {matchResult.Id} have invalid duration {duration} for analyze.{Environment.NewLine}It should be in range [{_settings.MinDuration}; {_settings.MaxDuration}]");
            return false;
        }
        return true;
    }

    private bool CheckPicks(T matchResult)
    {
        var radiantPicksCount = matchResult.PickBans.Count(e => e.IsPick && e.Team == TeamSide.Radiant);
        if (radiantPicksCount != _settings.NumberOfRadiantPicks)
        {
            _logger.LogWarning($"Match {matchResult.Id} have invalid number of radiant picks {radiantPicksCount} for analyze.{Environment.NewLine}It should be equal {_settings.NumberOfRadiantPicks}");
            return false;
        }
        var direPicksCount = matchResult.PickBans.Count(e => e.IsPick && e.Team == TeamSide.Dire);
        if (direPicksCount != _settings.NumberOfDirePicks)
        {
            _logger.LogWarning($"Match {matchResult.Id} have invalid number of dire picks {direPicksCount} for analyze.{Environment.NewLine}It should be equal {_settings.NumberOfDirePicks}");
            return false;
        }
        return true;
    }

    private bool CheckBans(T matchResult)
    {
        var radiantBansCount = matchResult.PickBans.Count(e => !e.IsPick && e.Team == TeamSide.Radiant);
        if (radiantBansCount > _settings.MaxNumberOfRadiantBans)
        {
            _logger.LogWarning($"Match {matchResult.Id} have invalid number of radiant bans {radiantBansCount} for analyze.{Environment.NewLine}It should be less than {_settings.MaxNumberOfRadiantBans}");
            return false;
        }
        var direBansCount = matchResult.PickBans.Count(e => !e.IsPick && e.Team == TeamSide.Dire);
        if (direBansCount > _settings.MaxNumberOfDireBans)
        {
            _logger.LogWarning($"Match {matchResult.Id} have invalid number of dire bans {direBansCount} for analyze.{Environment.NewLine}It should be less than {_settings.MaxNumberOfDireBans}");
            return false;
        }
        return true;
    }

    private bool CheckNumberOfHumanPlayers(T matchResult)
    {
        var humanPlayersCount = matchResult.HumanPlayersCount;
        if (humanPlayersCount != _settings.NumberOfHumanPlayers)
        {
            _logger.LogWarning($"Match {matchResult.Id} have invalid number of human players {humanPlayersCount} for analyze.{Environment.NewLine}It should be equal {_settings.NumberOfHumanPlayers}");
            return false;
        }
        return true;
    }

    private bool CheckLobbyType(T matchResult)
    {
        var lobbyType = matchResult.LobbyType;
        if (!_settings.AllowedLobbyTypes.Contains(lobbyType))
        {
            var sb = new StringBuilder();
            foreach (var allowedLobbyType in _settings.AllowedLobbyTypes)
            {
                sb.Append(allowedLobbyType.ToString() + ", ");
            }
            _logger.LogWarning($"Match {matchResult.Id} have invalid lobby type {lobbyType} for analyze.{Environment.NewLine}It should be one of {sb}");
            return false;
        }
        return true;
    }

    private bool CheckGameMode(T matchResult)
    {
        var gameMode = matchResult.GameMode;
        if (!_settings.AllowedGameModes.Contains(gameMode))
        {
            var sb = new StringBuilder();
            foreach (var allowedGameMode in _settings.AllowedGameModes)
            {
                sb.Append(allowedGameMode + ", ");
            }
            _logger.LogWarning($"Match {matchResult.Id} have invalid game mode {gameMode} for analyze.{Environment.NewLine}It should be one of {sb}");
            return false;
        }
        return true;
    }
}