﻿using Dummy_Server.Models;
using Segment;
using Segment.Model;

namespace Tracking_Service.Handlers
{
    public class TrackHandler : SpecHandler, IHandler
    {
        public async Task MakeCall(SpecMessage msg)
        {
            await CheckMessage(msg);
            Console.WriteLine(msg);
            Options options = AddErrorToContext(msg);
            msg.properties.Remove("event",out string @event);
            Dictionary<string, object> args = msg.properties.ToDictionary(pair => pair.Key, pair => (object)pair.Value);
            Analytics.Client.Track(msg.clientId, @event, args);
        }
    }
}
