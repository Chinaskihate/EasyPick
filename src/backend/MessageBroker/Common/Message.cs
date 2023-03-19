namespace MessageBroker.Common;

public class Message<T>
{
    public Message(T data, DateTime timeStamp)
    {
        Data = data;
        TimeStamp = timeStamp;
    }

    public T Data { get; }

    public DateTime TimeStamp { get; }
}