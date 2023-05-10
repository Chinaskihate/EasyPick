namespace Common.Data;

public interface IDataManager<in T>
{
    Task SaveAsync(IEnumerable<T> entities, CancellationToken ct);
}