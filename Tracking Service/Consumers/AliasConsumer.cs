using Gameball.MassTransit.DTOs.Segment;
using MassTransit;
using Tracking_Service.Handlers;

namespace Tracking_Service.Consumers
{
    public class AliasConsumer : IConsumer<AliasMessage>
    {
        private readonly ILogger<AliasConsumer> _logger;
        private readonly IHandler _handler;

        public AliasConsumer(ILogger<AliasConsumer> logger)
        {
            _logger = logger;
            _handler = new AliasHandler();
        }

        public async Task Consume(ConsumeContext<AliasMessage> context)
        {
            _logger.LogInformation("Alias, {ClientId}", context.Message.ClientId);
            await _handler.SendToTracker(context.Message);

        }
    }
}
