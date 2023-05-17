using Gameball.MassTransit.DTOs.Segment;
using MassTransit;
using Tracking_Service.Handlers;

namespace Tracking_Service.Consumers
{
    public class TrackConsumer : IConsumer<TrackMessage>
    {
        private readonly ILogger<TrackConsumer> _logger;
        private readonly IHandler _handler;

        public TrackConsumer(ILogger<TrackConsumer> logger)
        {
            _logger = logger;
            _handler = new TrackHandler();
        }

        public async Task Consume(ConsumeContext<TrackMessage> context)
        {
            _logger.LogInformation("Track, {ClientId}", context.Message.ClientId);
            await _handler.SendToTracker(context.Message);

        }
    }
}
