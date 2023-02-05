using Dummy_Server.Models;
using Segment;
using System.Collections.Generic;

namespace Tracking_Service.Handlers
{
    public class TrackHandler : SpecHandler
    {
        public void MakeCall(SpecMessage msg)
        {
            msg.properties.Remove("event",out string @event);
            Dictionary<string, object> args = msg.properties.ToDictionary(pair => pair.Key, pair => (object)pair.Value);
            Analytics.Client.Track(msg.clientId, @event, args);
        }
    }
}
