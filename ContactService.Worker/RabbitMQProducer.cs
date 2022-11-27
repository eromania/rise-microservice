using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace ContactService.Worker;

public static class RabbitMQProducer
{
    public static void SendMessage<T>(T message)
    {
        var queue = "report-response-queue";
        var factory = new ConnectionFactory {HostName = "localhost"};
        var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue);

        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);
        channel.BasicPublish(exchange: "", routingKey: queue, body: body);
    }
}