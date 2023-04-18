using System.Text;
using RabbitMQ.Client;

string hostname = "localhost";
string queueName = "exampleQueue";
string message = "Hello, this is Luis F Moreira using RabbitMQ!";
int interval = 1000; // interval in ms

var factory = new ConnectionFactory() { HostName = hostname };
using(var connection = factory.CreateConnection())
using(var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
    bool shouldContinue = true;
    while (shouldContinue)
    {
        var body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
        Console.WriteLine(" [x] Sent {0}", message);
        // Add a delay between messages
        System.Threading.Thread.Sleep(interval);
    }
}

