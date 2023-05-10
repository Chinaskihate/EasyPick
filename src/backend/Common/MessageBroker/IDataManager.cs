namespace MessageBroker;

public interface IDataManager<T>
{
    void Save(IEnumerable<T> entities, string path);
}