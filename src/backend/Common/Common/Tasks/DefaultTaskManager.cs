using Microsoft.Extensions.Logging;

namespace Common.Tasks;

public class DefaultTaskManager<T> : ITaskManager<T>
    where T : IDistributionTask
{
    private readonly T _task;
    private readonly ILogger _logger;
    private readonly CancellationTokenSource _cts;

    public DefaultTaskManager(
        T task,
        ILogger logger)
    {
        _task = task;
        _logger = logger;
        _cts = new CancellationTokenSource();
    }

    public async Task StartAsync()
    {
        try
        {
            await _task.RunAsync(_cts.Token);
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning($"System stopped {typeof(T).Name} task.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Inner processes stopped {typeof(T).Name} task.");
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public Task StopAsync()
    {
        _cts.Cancel();
        return Task.CompletedTask;
    }
}
