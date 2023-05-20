using Gameball.MassTransit.DTOs.Segment;
using MassTransit;
using Tracking_Service.Handlers;
using Tracking_Service.Managers.Interfaces;

namespace Tracking_Service.Consumers
{
    public class TrackConsumer : IConsumer<TrackMessage>
    {
        private readonly ILogger<TrackConsumer> _logger;
        private readonly ITrackManager _manager;

        public TrackConsumer(ILogger<TrackConsumer> logger, ITrackManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        public async Task Consume(ConsumeContext<TrackMessage> context)
        {
            _logger.LogInformation("Track, {ClientId}", context.Message.ClientId);
            await _manager.SendToTracker(context.Message);

        }
    }
}
