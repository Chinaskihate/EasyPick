namespace MessageBroker.Common;

public interface IMessageProducer<in T>
{
    Task SendAsync(T message);
}