using Segment;
using Segment.Model;
using Dummy_Server.Models;
using Tracking_Service.Services;

namespace Tracking_Service.Handlers
{
    public class IdentifyHandler : SpecHandler
    {
        public void MakeCall(SpecMessage msg)
        {
            msg.properties.Remove("error", out string errorMsg);
            Options options = new Options();
            if (errorMsg != null)
            {
                Context context = new Context();
                context = context.Add("error", errorMsg);
                options.SetContext(context);
            }
            Dictionary<string, object> args = msg.properties.ToDictionary(pair => pair.Key, pair => (object)pair.Value);
            Analytics.Client.Identify(msg.clientId, args, options);
        }
    }
}
