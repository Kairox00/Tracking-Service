using Dummy_Server.Models;
using Segment;
using Segment.Model;

namespace Tracking_Service.Handlers
{
    public class AliasHandler : SpecHandler, IHandler
    {
        public async Task SendToSegment(SpecMessage msg)
        {
            Dictionary<string, object> dict = await ProcessMessage(msg);
            Analytics.Client.Alias(msg.clientId, (string)dict["newId"], (Options)dict["options"]);
        }
    }
}
