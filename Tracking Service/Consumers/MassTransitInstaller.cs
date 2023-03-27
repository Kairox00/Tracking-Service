using Gameball.MassTransit;
using MassTransit;

namespace Tracking_Service.Consumers
{
    public static class MassTransitInstaller
    {
        public static void InstallMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqSetting = new RabbitMqSettings();
            configuration.Bind(nameof(RabbitMqSettings), rabbitMqSetting);

            services.AddMassTransit(x =>
            {
                x.AddConsumer<TrackConsumer>();
                x.AddConsumer<IdentifyConsumer>();
                x.AddConsumer<GroupConsumer>();
                x.AddConsumer<AliasConsumer>();
                x.AddConsumer<TestConsumer>();

                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host($"{rabbitMqSetting.Protocol}://{rabbitMqSetting.UserName}:{rabbitMqSetting.Password}@{rabbitMqSetting.HostName}:{rabbitMqSetting.Port}");
                    // cfg.Host(rabbitMqSetting.HostName, h =>
                    // {
                    //   h.Username(rabbitMqSetting.UserName);
                    //   h.Password(rabbitMqSetting.Password);
                    // });

                    //cfg.MessageTopology.SetEntityNameFormatter(new BusEnvironmentNameFormatter(cfg.MessageTopology.EntityNameFormatter));
                    cfg.ReceiveEndpoint(MessageQueueConstants.TrackQueue, c =>
                    {
                        //c.ClearSerialization();
                        //c.UseRawJsonSerializer();
                        c.ConfigureConsumer<TrackConsumer>(ctx);
                        // To ignore skipped queue
                        c.DiscardSkippedMessages();
                    });

                    cfg.ReceiveEndpoint(MessageQueueConstants.IdentifyQueue, c =>
                    {
                        c.Bind(MessageQueueConstants.IdentifyQueue);
                        //c.ClearSerialization();
                        //c.UseRawJsonSerializer();
                        c.ConfigureConsumer<IdentifyConsumer>(ctx);
                        // To ignore skipped queue
                        c.DiscardSkippedMessages();
                    });

                    cfg.ReceiveEndpoint(MessageQueueConstants.TestQueue, c =>
                    {
                        c.Bind(MessageQueueConstants.TestQueue);
                        c.ClearSerialization();
                        c.UseRawJsonSerializer();
                        c.UseRawJsonDeserializer();
                        c.ConfigureConsumer<TestConsumer>(ctx);
                        // To ignore skipped queue
                        c.DiscardSkippedMessages();
                    });

                    cfg.ReceiveEndpoint(MessageQueueConstants.GroupQueue, c =>
                    {
                        //c.ClearSerialization();
                        //c.UseRawJsonSerializer();
                        c.ConfigureConsumer<GroupConsumer>(ctx);
                        // To ignore skipped queue
                        c.DiscardSkippedMessages();
                    });

                    cfg.ReceiveEndpoint(MessageQueueConstants.AliasQueue, c =>
                    {
                        //c.ClearSerialization();
                        //c.UseRawJsonSerializer();
                        c.ConfigureConsumer<AliasConsumer>(ctx);
                        // To ignore skipped queue
                        c.DiscardSkippedMessages();
                    });

                });

            });

            /*services.AddMassTransitHostedService();*/
            // General Configuration
            services.AddScoped<TrackConsumer>();
            services.AddScoped<IdentifyConsumer>();
            services.AddScoped<GroupConsumer>();
            services.AddScoped<AliasConsumer>();
            services.AddScoped<TestConsumer>();


            Console.WriteLine("MassTransitInstalled");
        }
    }

}
