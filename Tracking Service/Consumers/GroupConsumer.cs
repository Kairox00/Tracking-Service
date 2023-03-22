﻿using Gameball.MassTransit;
using MassTransit;
using Tracking_Service.Handlers;

namespace Tracking_Service.Consumers
{
    public class GroupConsumer : IConsumer<SpecMessage>
    {
        private readonly ILogger<GroupConsumer> _logger;
        private readonly IHandler _handler;

        public GroupConsumer(ILogger<GroupConsumer> logger)
        {
            _logger = logger;
            _handler = new GroupHandler();
        }

        public async Task Consume(ConsumeContext<SpecMessage> context)
        {
            _logger.LogInformation("Group, {clientId}", context.Message.clientId);
            await _handler.SendToTracker(context.Message);

        }
    }
}