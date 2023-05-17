using Gameball.MassTransit;
using Gameball.MassTransit.DTOs.Segment;
using MassTransit;

namespace Tracking_Service.Consumers
{
    [EntityName(MessageQueueConstants.GroupQueue)]
    public class GroupMessage : SpecMessage
    {
        public GroupMessage(string ClientId, Dictionary<string, object> Properties) : base(ClientId, Properties)
        {
        }
    }
}
