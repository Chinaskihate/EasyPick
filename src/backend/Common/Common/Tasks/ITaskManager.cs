namespace Common.Tasks;

public interface ITaskManager<T>
    where T : IDistributionTask
{
    public Task StartAsync();

    public Task StopAsync();
}

