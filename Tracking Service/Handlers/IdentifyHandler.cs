using Segment;
using Segment.Model;
using Dummy_Server.Models;

namespace Tracking_Service.Handlers
{
    public class IdentifyHandler : SpecHandler
    {
        public void MakeCall(SpecMessage msg)
        {
            var traits = base.ParseDictValues(msg.traits, msg.DataTypes);
            Analytics.Client.Identify(msg.userId, traits);
        }
    }
}
