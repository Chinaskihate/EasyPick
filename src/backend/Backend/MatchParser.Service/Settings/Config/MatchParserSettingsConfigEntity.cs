using MatchParser.Contract.Settings.Config;

namespace MatchParser.Service.Settings.Config;

public class MatchParserSettingsConfigEntity
{
    public MatchSettingsConfigEntity MatchSettings { get; set; }

    public BatchSettingsConfigEntity BatchSettings { get; set; }

    public string MatchesDataUrl { get; set; }

    public string PathToDirectory { get; set; }

    public string DbConnectionString { get; set; }
}