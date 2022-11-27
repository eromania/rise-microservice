﻿using System.Net.Http.Headers;
using System.Text;
using ContactService.Worker;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

//subscribe for the report requested event
var queue = "report-request-queue";
var factory = new ConnectionFactory {HostName = "localhost"};
var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
channel.QueueDeclare(queue, false, false, false, null);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += async (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine(" [x] Received {0}", message);

    await ProcessRepositoriesAsync(client);
};

channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);

static async Task ProcessRepositoriesAsync(HttpClient client)
{
    //get the json data from the contact service report api
    var json = await client.GetStringAsync("http://localhost:5005/api/report/data-by-location");

    //send reportresponse message to the queue
    RabbitMQProducer.SendMessage(json);
    
    Console.WriteLine(json);
}


Console.ReadKey();