using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using Segment;
using Microsoft.AspNetCore.Connections;
using Tracking_Service.Handlers;

namespace Receive;
public class Receive
{
    public Receive()
    {
        Analytics.Initialize("80CAseuAotXl7sEzy8aDbL3BGt8pAZ0q");
    }

    public void startReceiving()
    {
        //MessageHandler handler = new MessageHandler();
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "queue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            Console.WriteLine("Listening");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);
                new IdentifyHandler().MakeCall(message);
            };
            channel.BasicConsume(queue: "queue",
                                 autoAck: true,
                                 consumer: consumer);


            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
    public static void Main()
    {
        Receive receiver = new Receive();
        receiver.startReceiving();
    }
}