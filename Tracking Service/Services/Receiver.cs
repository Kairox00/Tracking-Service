using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using Segment;
using Microsoft.AspNetCore.Connections;
using Tracking_Service.Handlers;

namespace Tracking_Service.Receivers;
public class Receiver
{
    public Receiver()
    {

    }


    private static void ListenOnQueue(IModel channel, String Queue)
    {
        channel.QueueDeclare(queue: Queue,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        Console.WriteLine($"{Queue} queue declared");
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (sender, args) =>
        {
            var body = args.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($" [x] Received on {Queue} {message}");
            new SpecHandler().MakeCall(message);
            
        };
        channel.BasicConsume(Queue, autoAck: true, consumer: consumer);
    }

    public static void Run()
    {
        Analytics.Initialize("xOYECODe4mKLEVWyF5ZGoE04cU8CxnTj");
        var connectionFactory = new ConnectionFactory();
        connectionFactory.Uri = new Uri("amqp://localhost");

        using var connection = connectionFactory.CreateConnection();
        using var channel = connection.CreateModel();

        // Create and start both listeners
        Task.Run(() => ListenOnQueue(channel, "Identify"));
        Task.Run(() => ListenOnQueue(channel, "Track"));

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }

}