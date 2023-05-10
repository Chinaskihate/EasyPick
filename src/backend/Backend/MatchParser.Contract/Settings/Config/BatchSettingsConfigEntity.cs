namespace MatchParser.Contract.Settings.Config;

public class BatchSettingsConfigEntity
{
    public int StepSize { get; set; }

    public long StartId { get; set; }

    public int BufferSize { get; set; }

    public int RetryIntervalInSec { get; set; }
}