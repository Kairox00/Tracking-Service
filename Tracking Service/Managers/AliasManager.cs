using Gameball.MassTransit.DTOs.Segment;
using Segment;
using Segment.Model;
using Tracking_Service.Cache;
using Tracking_Service.Managers.Interfaces;

namespace Tracking_Service.Managers
{
    public class AliasManager : IAliasManager
    {
        private ICacheService _cacheService;
        private IShared _shared;

        public AliasManager(ICacheService cacheService, IShared shared)
        {
            _cacheService = cacheService;
            _shared = shared;
        }

        public async Task SendToTracker(SpecMessage msg)
        {
            Dictionary<string, object> data = await _shared.ProcessMessage(msg);
            if (Validate(data))
            {
                Analytics.Client.Alias(msg.ClientId, (string)data["newId"], (Options)data["options"]);
            }
        }

        public bool Validate(Dictionary<string, object> data)
        {
            return Shared.Validate(data)
                && (data["newId"] != null && data["newId"].GetType() == typeof(string));
        }
    }
}
