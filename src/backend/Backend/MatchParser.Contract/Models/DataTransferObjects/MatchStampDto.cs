using System.Text.Json.Serialization;

namespace MatchParser.Contract.Models.DataTransferObjects;

public class MatchStampDto
{
    [JsonPropertyName("match_id")]
    public long MatchId { get; set; }
}