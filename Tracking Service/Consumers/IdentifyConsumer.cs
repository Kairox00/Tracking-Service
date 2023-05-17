using Gameball.MassTransit.DTOs.Segment;
using MassTransit;
using Tracking_Service.Handlers;

namespace Tracking_Service.Consumers
{
    public class IdentifyConsumer : IConsumer<IdentifyMessage>
    {
        private readonly ILogger<IdentifyConsumer> _logger;
        private readonly IHandler _handler;

        public IdentifyConsumer(ILogger<IdentifyConsumer> logger)
        {
            _logger = logger;
            _handler = new IdentifyHandler();
        }

        public async Task Consume(ConsumeContext<IdentifyMessage> context)
        {
            var message = context.Message;
            _logger.LogInformation("Identify, {ClientId}", context.Message.ClientId);
            await new IdentifyHandler().SendToTracker(context.Message);

        }
    }
}
