using Gameball.MassTransit.DTOs.Segment;
using MassTransit;
using Tracking_Service.Handlers;
using Tracking_Service.Managers.Interfaces;

namespace Tracking_Service.Consumers
{
    public class GroupConsumer : IConsumer<GroupMessage>
    {
        private readonly ILogger<GroupConsumer> _logger;
        private readonly IGroupManager _manager;

        public GroupConsumer(ILogger<GroupConsumer> logger, IGroupManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        public async Task Consume(ConsumeContext<GroupMessage> context)
        {
            _logger.LogInformation("Group, {ClientId}", context.Message.ClientId);
            await _manager.SendToTracker(context.Message);

        }
    }
}
