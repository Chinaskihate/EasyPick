using System.Text;
using Common.Data;
using MatchParser.Contract.Models;
using MatchParser.Contract.Settings.Entities;
using Microsoft.Extensions.Logging;

namespace MatchParser.Storage.CsvManager;

public class CsvManager : IDataManager<MatchResult>
{
    private readonly string _pathToDirectory;
    private readonly MatchSettings _settings;
    private readonly ILogger _logger;

    public CsvManager(string pathToDirectory, MatchSettings settings, ILogger logger)
    {
        _pathToDirectory = pathToDirectory;
        _settings = settings;
        _logger = logger;
    }

    public async Task SaveAsync(IEnumerable<MatchResult> entities, CancellationToken ct = default)
    {
        var results = entities.ToList();
        var maxId = results.Max(r => r.Id);
        var path = Path.Combine(_pathToDirectory, maxId + ".csv");
        _logger.LogInformation($"Saving {results.Count} elements to {path}");
        var sb = new StringBuilder();
        InitColumns(sb);
        FillTable(sb, results);

        await File.AppendAllTextAsync(path, sb.ToString(), ct);
    }

    private void InitColumns(StringBuilder sb)
    {
        sb.Append("match_id,radiant_win");
        for (var i = 0; i < _settings.NumberOfRadiantPicks; i++)
        {
            sb.Append($",radiant_pick_{i + 1}");
        }
        for (var i = 0; i < _settings.NumberOfDirePicks; i++)
        {
            sb.Append($",dire_pick_{i + 1}");
        }
        for (var i = 0; i < _settings.MaxNumberOfRadiantBans; i++)
        {
            sb.Append($",radiant_ban_{i + 1}");
        }

        for (var i = 0; i < _settings.MaxNumberOfDireBans; i++)
        {
            sb.Append($",dire_ban_{i + 1}");
        }
        sb.AppendLine();
    }

    private void FillTable(StringBuilder sb, IEnumerable<MatchResult> results)
    {
        foreach (var result in results)
        {
            sb.Append($"{result.Id}");
            sb.Append($",{(result.RadiantWin ? 1 : 0)}");
            var radiantPicks = result.PickBans
                .Where(pb => pb.Team == TeamSide.Radiant && pb.IsPick)
                .ToList();
            for (var i = 0; i < _settings.NumberOfRadiantPicks; i++)
            {
                if (i < radiantPicks.Count)
                {
                    sb.Append($",{radiantPicks[i].HeroId}");
                }
                else
                {
                    sb.Append(",-1");
                }
            }

            var direPicks = result.PickBans
                .Where(pb => pb.Team == TeamSide.Dire && pb.IsPick)
                .ToList();
            for (var i = 0; i < _settings.NumberOfRadiantPicks; i++)
            {
                if (i < direPicks.Count)
                {
                    sb.Append($",{direPicks[i].HeroId}");
                }
                else
                {
                    sb.Append(",-1");
                }
            }

            var radiantBans = result.PickBans
                .Where(pb => pb.Team == TeamSide.Radiant && !pb.IsPick)
                .ToList();
            for (var i = 0; i < _settings.MaxNumberOfRadiantBans; i++)
            {
                if (i < radiantBans.Count)
                {
                    sb.Append($",{radiantBans[i].HeroId}");
                }
                else
                {
                    sb.Append(",-1");
                }
            }

            var direBans = result.PickBans
                .Where(pb => pb.Team == TeamSide.Dire && !pb.IsPick)
                .ToList();
            for (var i = 0; i < _settings.MaxNumberOfDireBans; i++)
            {
                if (i < direBans.Count)
                {
                    sb.Append($",{direBans[i].HeroId}");
                }
                else
                {
                    sb.Append(",-1");
                }
            }

            sb.AppendLine();
        }
    }
}