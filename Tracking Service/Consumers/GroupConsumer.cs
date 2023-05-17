using Gameball.MassTransit.DTOs.Segment;
using MassTransit;
using Tracking_Service.Handlers;

namespace Tracking_Service.Consumers
{
    public class GroupConsumer : IConsumer<GroupMessage>
    {
        private readonly ILogger<GroupConsumer> _logger;
        private readonly IHandler _handler;

        public GroupConsumer(ILogger<GroupConsumer> logger)
        {
            _logger = logger;
            _handler = new GroupHandler();
        }

        public async Task Consume(ConsumeContext<GroupMessage> context)
        {
            _logger.LogInformation("Group, {ClientId}", context.Message.ClientId);
            await _handler.SendToTracker(context.Message);

        }
    }
}
