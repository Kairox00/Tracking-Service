using Gameball.MassTransit;
using MassTransit;
using Tracking_Service.Handlers;

namespace Tracking_Service.Consumers
{
    public class TrackConsumer : IConsumer<SpecMessage>
    {
        private readonly ILogger<TrackConsumer> _logger;
        private readonly IHandler _handler;

        public TrackConsumer(ILogger<TrackConsumer> logger)
        {
            _logger = logger;
            _handler = new TrackHandler();
        }

        public async Task Consume(ConsumeContext<SpecMessage> context)
        {
            _logger.LogInformation("Track, {clientId}", context.Message.clientId);
            await _handler.SendToTracker(context.Message);

        }
    }
}
