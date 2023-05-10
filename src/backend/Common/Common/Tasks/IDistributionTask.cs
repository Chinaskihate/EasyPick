namespace Common.Tasks;

public interface IDistributionTask
{
    Task RunAsync(CancellationToken ct); 
}
