﻿using Gameball.MassTransit.DTOs.Segment;
using Segment;
using Segment.Model;
using Tracking_Service.Cache;
using Tracking_Service.Managers.Interfaces;

namespace Tracking_Service.Managers
{
    public class IdentifyManager : IIdentifyManager
    {
        private IShared _shared;

        public IdentifyManager(IShared shared)
        {
            _shared = shared;
        }

        public async Task SendToTracker(SpecMessage msg)
        {
            Dictionary<string, object> data = await _shared.ProcessMessage(msg);
            if (Validate(data))
            {
                Analytics.Client.Identify(msg.ClientId, (IDictionary<string, object>)data["args"], (Options)data["options"]);
            }
        }

        public bool Validate(Dictionary<string, object> data)
        {
            return Shared.Validate(data);
        }
    }
}
