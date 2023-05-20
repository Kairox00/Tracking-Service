using Gameball.MassTransit.DTOs.Segment;
using Segment;
using Segment.Model;
using Tracking_Service.Cache;
using Tracking_Service.Managers.Interfaces;

namespace Tracking_Service.Managers
{
    public class TrackManager : ITrackManager
    {
        private IShared _shared;

        public TrackManager(IShared shared)
        {
            _shared = shared;
        }

        public async Task SendToTracker(SpecMessage msg)
        {
            Dictionary<string, object> data = await _shared.ProcessMessage(msg);
            if (Validate(data))
            {
                Analytics.Client.Track(msg.ClientId, (string)data["event"], (IDictionary<string, object>)data["args"], (Options)data["options"]);
            }

        }

        public bool Validate(Dictionary<string, object> data)
        {
            return Shared.Validate(data)
                && (data["event"] != null && data["event"].GetType() == typeof(string));
        }
    }
}
