using System.Text.Json.Serialization;

namespace MatchParser.Contract.Models.DataTransferObjects;

public class PickBanDto
{
    [JsonPropertyName("is_pick")]
    public bool IsPick { get; set; }

    [JsonPropertyName("hero_id")]
    public int HeroId { get; set; }

    [JsonPropertyName("team")]
    public int Team { get; set; }
}