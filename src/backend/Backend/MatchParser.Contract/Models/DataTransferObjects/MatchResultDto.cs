using System.Text.Json.Serialization;

namespace MatchParser.Contract.Models.DataTransferObjects;

public class MatchResultDto
{
    [JsonPropertyName("match_id")]
    public long Id { get; set; }

    [JsonPropertyName("radiant_win")]
    public bool RadiantWin { get; set; }

    public int Duration { get; set; }

    [JsonPropertyName("game_mode")]
    public int GameMode { get; set; }

    [JsonPropertyName("lobby_type")]
    public int LobbyType { get; set; }

    [JsonPropertyName("dire_score")]
    public int DireScore { get; set; }

    [JsonPropertyName("radiant_score")]
    public int RadiantScore { get; set; }

    [JsonPropertyName("human_players")]
    public int HumanPlayersCount { get; set; }

    [JsonPropertyName("picks_bans")]
    public List<PickBanDto> PickBans { get; set; }
}