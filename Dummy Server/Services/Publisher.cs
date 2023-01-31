using Microsoft.AspNetCore.Connections;
using System;
using RabbitMQ.Client;
using System.Text;

namespace Dummy_Server.Services
{
    public class Publisher
    {
        public void Publish(string Queue, string Message)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "localhost";
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: Queue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(Message);

                channel.BasicPublish(exchange: "",
                                     routingKey: Queue,
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", Message);
            }

        }
    }

}
