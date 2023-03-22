using Gameball.MassTransit;
using Segment;
using Segment.Model;

namespace Tracking_Service.Handlers
{
    public class AliasHandler : SpecHandler, IHandler
    {
        public async Task SendToTracker(SpecMessage msg)
        {
            Dictionary<string, object> data = await ProcessMessage(msg);
            if (Validate(data))
            {
                Analytics.Client.Alias(msg.clientId, (string)data["newId"], (Options)data["options"]);
            }
        }

        private new bool Validate(Dictionary<string, object> data)
        {
            return base.Validate(data)
                && (data["newId"] != null && data["newId"].GetType() == typeof(string));
        }
    }
}
