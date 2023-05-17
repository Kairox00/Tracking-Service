using Gameball.MassTransit.DTOs.Segment;
using Segment;
using Segment.Model;

namespace Tracking_Service.Handlers
{
    public class GroupHandler : SpecHandler, IHandler
    {
        public async Task SendToTracker(SpecMessage msg)
        {
            Dictionary<string, object> data = await ProcessMessage(msg);
            if (Validate(data))
            {
                Analytics.Client.Group(msg.ClientId, (string)data["groupId"], (IDictionary<string, object>)data["args"], (Options)data["options"]);
            }
           
        }

        private new bool Validate(Dictionary<string, object> data)
        {
            return base.Validate(data)
                && (data["groupId"] != null && data["groupId"].GetType() == typeof(string));
        }
    }
}
