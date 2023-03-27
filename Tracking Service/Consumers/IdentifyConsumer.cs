﻿using Gameball.MassTransit;
using MassTransit;
using Tracking_Service.Handlers;

namespace Tracking_Service.Consumers
{
    public class IdentifyConsumer : IConsumer<SpecMessage>
    {
        private readonly ILogger<IdentifyConsumer> _logger;
        //private readonly IHandler _handler;

        public IdentifyConsumer(ILogger<IdentifyConsumer> logger)
        {
            _logger = logger;
 //           _handler = new IdentifyHandler();
        }

        public async Task Consume(ConsumeContext<SpecMessage> context)
        {
            var message = context.Message;
            _logger.LogInformation("Identify, {ClientId}", context.Message.ClientId);
          //  await _handler.SendToTracker(context.Message);

        }
    }
}
