using Dummy_Server.Models;
using Segment;
using Segment.Model;

namespace Tracking_Service.Handlers
{
    public class TrackHandler : SpecHandler, IHandler
    {
        public async Task SendToTracker(SpecMessage msg)
        {
            Dictionary<string, object> data = await ProcessMessage(msg);
            if (Validate(data))
            {
                Analytics.Client.Track(msg.clientId, (string)data["event"], (IDictionary<string, object>)data["args"], (Options)data["options"]);
            }
            
        }

        private new bool Validate(Dictionary<string, object> data)
        {
            return base.Validate(data)
                && (data["event"] != null && data["event"].GetType() == typeof(string));
        }
    }
}
