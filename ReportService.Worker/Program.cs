using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReportService.Worker;


// var message = "\"{\"reportId\":37,\"data\":[{\"location\":\"ankara\",\"userCount\":1,\"telephoneCount\":1}]}\"";
// var json = message.Replace('\\', ' ').Trim().Substring(1, message.Length - 2).Replace("\"", "'");
// var reportId = JObject.Parse(json)["reportId"];//!.First().Value<string>();




using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();

//subscribe for the report requested event
var queue = "report-response-queue";
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

    await ProcessRepositoriesAsync(client, message);
};

channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);

static async Task ProcessRepositoriesAsync(HttpClient client, string message)
{
    try
    {
        //deserialize the message
        var reportVm = JsonConvert.DeserializeObject<ReportVm>(message);
        
        //put message to the report service
        var response = await client.PutAsJsonAsync($"http://localhost:5007/api/reports/{reportVm!.ReportId}", message);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }

    Console.WriteLine("report prepared");
}


Console.ReadKey();