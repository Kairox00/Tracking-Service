using Gameball.MassTransit.DTOs.Segment;
using MassTransit;
using Tracking_Service.Handlers;
using Tracking_Service.Managers;
using Tracking_Service.Managers.Interfaces;

namespace Tracking_Service.Consumers
{
    public class IdentifyConsumer : IConsumer<IdentifyMessage>
    {
        private readonly ILogger<IdentifyConsumer> _logger;
        private IIdentifyManager _manager;

        public IdentifyConsumer(ILogger<IdentifyConsumer> logger, IIdentifyManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        public async Task Consume(ConsumeContext<IdentifyMessage> context)
        {
            var message = context.Message;
            _logger.LogInformation("Identify, {ClientId}", context.Message.ClientId);
            await _manager.SendToTracker(context.Message);

        }

    }
}
