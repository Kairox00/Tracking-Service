using MassTransit;
using Newtonsoft.Json;

namespace Gameball.MassTransit
{

    [EntityName(MessageQueueConstants.TestQueue)]
    public class TestMessage
    {
        public string ClientId { get; set; }

        public Dictionary<string, string> Properties { get; set; }

    }
}
