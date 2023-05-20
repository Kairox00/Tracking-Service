using Gameball.MassTransit.DTOs.Segment;
using MassTransit;
using Tracking_Service.Handlers;
using Tracking_Service.Managers.Interfaces;

namespace Tracking_Service.Consumers
{
    public class AliasConsumer : IConsumer<AliasMessage>
    {
        private readonly ILogger<AliasConsumer> _logger;
        private readonly IAliasManager _manager;

        public AliasConsumer(ILogger<AliasConsumer> logger, IAliasManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        public async Task Consume(ConsumeContext<AliasMessage> context)
        {
            _logger.LogInformation("Alias, {ClientId}", context.Message.ClientId);
            await _manager.SendToTracker(context.Message);

        }
    }
}
