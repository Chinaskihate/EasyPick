namespace DraftPrediction.Contract.Models.DataTransferObjects.Drafts;

public class RecommendedDraftDto
{
    public int HeroId { get; set; }
    
    public double WinProbability { get; set; }
}