using MassTransit;
using Newtonsoft.Json;

namespace Gameball.MassTransit.DTOs.Segment
{

    [EntityName(MessageQueueConstants.TestQueue)]
    public class TestMessage : SpecMessage
    {
        public TestMessage(string ClientId, Dictionary<string, object> Properties) : base(ClientId, Properties)
        {
        }
    }
}
