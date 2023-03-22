using Gameball.MassTransit;
using MassTransit;
using Tracking_Service.Handlers;

namespace Tracking_Service.Consumers
{
    public class AliasConsumer : IConsumer<SpecMessage>
    {
        private readonly ILogger<AliasConsumer> _logger;
        private readonly IHandler _handler;

        public AliasConsumer(ILogger<AliasConsumer> logger)
        {
            _logger = logger;
            _handler = new AliasHandler();
        }

        public async Task Consume(ConsumeContext<SpecMessage> context)
        {
            _logger.LogInformation("Alias, {clientId}", context.Message.clientId);
            await _handler.SendToTracker(context.Message);

        }
    }
}
