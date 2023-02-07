using Dummy_Server.Models;
using Segment;
using Segment.Model;

namespace Tracking_Service.Handlers
{
    public class AliasHandler : SpecHandler, IHandler
    {
        public async Task MakeCall(SpecMessage msg)
        {
            await CheckMessage(msg);
            Console.WriteLine(msg);
            Options options = AddErrorToContext(msg);
            msg.properties.Remove("newId", out string newId);
            Analytics.Client.Alias(msg.clientId, newId, options);
        }
    }
}
