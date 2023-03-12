using Segment;
using Segment.Model;
using Dummy_Server.Models;

namespace Tracking_Service.Handlers
{
    public class IdentifyHandler : SpecHandler, IHandler
    {
        public async Task SendToSegment(SpecMessage msg)
        {
            Dictionary<string, object> dict = await ProcessMessage(msg);
            Analytics.Client.Identify(msg.clientId, (IDictionary<string, object>)dict["args"], (Options)dict["options"]);
        }
    }
}
