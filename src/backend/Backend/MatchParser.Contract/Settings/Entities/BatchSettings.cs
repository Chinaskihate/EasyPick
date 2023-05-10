namespace MatchParser.Contract.Settings.Entities;

public class BatchSettings
{
    public int StepSize { get; set; }

    public long StartId { get; set; }

    public int BufferSize { get; set; }

    public TimeSpan RetryInterval { get; set; }
}