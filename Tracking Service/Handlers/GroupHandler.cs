using Dummy_Server.Models;
using Segment;
using Segment.Model;

namespace Tracking_Service.Handlers
{
    public class GroupHandler : SpecHandler, IHandler
    {
        public async Task MakeCall(SpecMessage msg)
        {
            await CheckMessage(msg);
            Console.WriteLine(msg);
            Options options = AddErrorToContext(msg);
            msg.properties.Remove("groupId", out string groupId);
            Dictionary<string, object> args = msg.properties.ToDictionary(pair => pair.Key, pair => (object)pair.Value);
            Analytics.Client.Group(msg.clientId, groupId, args);
        }
    }
}
