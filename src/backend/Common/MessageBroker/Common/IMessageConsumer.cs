namespace MessageBroker.Common;

public interface IMessageConsumer<T>
{
    Task ExecuteAsync(Func<T, Task> callback, CancellationToken ct);
}