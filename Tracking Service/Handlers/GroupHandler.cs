using Dummy_Server.Models;
using Segment;
using Segment.Model;

namespace Tracking_Service.Handlers
{
    public class GroupHandler : SpecHandler, IHandler
    {
        public async Task SendToSegment(SpecMessage msg)
        {
            Dictionary<string, object> dict = await ProcessMessage(msg);
            Analytics.Client.Group(msg.clientId, (string)dict["groupId"], (IDictionary<string, object>)dict["args"], (Options)dict["options"]);
        }
    }
}
