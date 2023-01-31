using Dummy_Server.Models;
using Segment;

namespace Tracking_Service.Handlers
{
    public class TrackHandler : SpecHandler
    {
        public void MakeCall(SpecMessage msg)
        {
            var properties = base.ParseDictValues(msg.properties, msg.DataTypes);

            Analytics.Client.Track(msg.userId, msg.@event, properties);
        }
    }
}
