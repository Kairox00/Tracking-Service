using Segment;
using Segment.Model;
using Gameball.MassTransit;

namespace Tracking_Service.Handlers
{
    public class IdentifyHandler : SpecHandler, IHandler
    {
        public async Task SendToTracker(SpecMessage msg)
        {
            Dictionary<string, object> data = await ProcessMessage(msg);
            if (Validate(data))
            {
                Analytics.Client.Identify(msg.clientId, (IDictionary<string, object>)data["args"], (Options)data["options"]);
            }
        }

    }
}
