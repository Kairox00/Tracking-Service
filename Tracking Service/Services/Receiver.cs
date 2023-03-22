using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using Segment;
using Microsoft.AspNetCore.Connections;
using Tracking_Service.Handlers;
using System.Text.Json;
using Gameball.MassTransit;

namespace Tracking_Service.Receivers;
public class Receiver
{
    private static void ListenOnQueue(IModel channel, String Queue, IHandler handler)
    {
        channel.QueueDeclare(queue: Queue,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        Console.WriteLine($"{Queue} queue declared");
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (sender, args) =>
        {
            var body = args.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($" [x] Received on {Queue} {message}");
            SpecMessage msg = JsonSerializer.Deserialize<SpecMessage>(message);
            await handler.SendToTracker(msg);
            
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
        Task.Run(() => ListenOnQueue(channel, "Identify", new IdentifyHandler()));
        Task.Run(() => ListenOnQueue(channel, "Track", new TrackHandler()));
        Task.Run(() => ListenOnQueue(channel, "Group", new GroupHandler()));
        Task.Run(() => ListenOnQueue(channel, "Alias", new AliasHandler()));

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }

}