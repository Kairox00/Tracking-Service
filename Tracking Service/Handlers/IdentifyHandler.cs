﻿using Segment;
using Segment.Model;
using Dummy_Server.Models;

namespace Tracking_Service.Handlers
{
    public class IdentifyHandler : SpecHandler, IHandler
    {
        public async Task MakeCall(SpecMessage msg)
        {
            await CheckMessage(msg);
            Console.WriteLine(msg);
            Options options = AddErrorToContext(msg);
            Dictionary<string, object> args = msg.properties.ToDictionary(pair => pair.Key, pair => (object)pair.Value);
            Analytics.Client.Identify(msg.clientId, args, options);
        }
    }
}
