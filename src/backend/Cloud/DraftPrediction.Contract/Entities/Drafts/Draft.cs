namespace DraftPrediction.Contract.Entities.Drafts;

public class Draft
{
    public Hero Hero { get; set; } = null!;

    public double Rate { get; set; }
}