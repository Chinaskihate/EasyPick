using MatchParser.Contract.Settings.Entities;

namespace MatchParser.Service.Settings.Entities;

public class MatchParserSettings
{
    public MatchSettings MatchSettings { get; set; }

    public BatchSettings BatchSettings { get; set; }

    public string PathToDirectory { get; set; }
    
    public string MatchesDataUrl { get; set; }

    public string DbConnectionString {get; set; }
}