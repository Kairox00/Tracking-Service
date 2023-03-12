using Dummy_Server.Models;
using Segment;
using Segment.Model;

namespace Tracking_Service.Handlers
{
    public class TrackHandler : SpecHandler, IHandler
    {
        public async Task SendToSegment(SpecMessage msg)
        {
            Dictionary<string, object> dict = await ProcessMessage(msg);
            Analytics.Client.Track(msg.clientId, (string)dict["event"], (IDictionary<string, object>)dict["args"], (Options)dict["options"]);
        }
    }
}
