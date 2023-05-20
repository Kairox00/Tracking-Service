using Gameball.MassTransit.DTOs.Segment;
using Segment;
using Segment.Model;
using Tracking_Service.Cache;
using Tracking_Service.Managers.Interfaces;

namespace Tracking_Service.Managers
{
    public class GroupManager : IGroupManager
    {
        private IShared _shared;

        public GroupManager(IShared shared)
        {
            _shared = shared;
        }

        public async Task SendToTracker(SpecMessage msg)
        {
            Dictionary<string, object> data = await _shared.ProcessMessage(msg);
            if (Validate(data))
            {
                Analytics.Client.Group(msg.ClientId, (string)data["groupId"], (IDictionary<string, object>)data["args"], (Options)data["options"]);
            }

        }

        public bool Validate(Dictionary<string, object> data)
        {
            return Shared.Validate(data)
                && (data["groupId"] != null && data["groupId"].GetType() == typeof(string));
        }
    }
}
