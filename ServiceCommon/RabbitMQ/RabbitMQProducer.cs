using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using ServiceCommon.Interfaces;

namespace ServiceCommon.RabbitMQ;

public class RabbitMQProducer : IMessageProducer
{
    public void SendMessage<T>(T message, string queueName)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            channel.BasicPublish(exchange: "",
                                 routingKey: queueName,
                                 basicProperties: null,
                                 body: body);
        }
    }
}