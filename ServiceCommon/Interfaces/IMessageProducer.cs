namespace ServiceCommon.Interfaces;

public interface IMessageProducer
{
    void SendMessage<T> (T message, string queueName); 
}