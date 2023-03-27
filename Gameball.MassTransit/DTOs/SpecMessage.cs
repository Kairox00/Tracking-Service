using MassTransit;
using Newtonsoft.Json;

namespace Gameball.MassTransit
{

    [EntityName(MessageQueueConstants.IdentifyQueue)]
    public class SpecMessage
    {
        [JsonProperty(PropertyName = "clientId")]
        public string ClientId { get; set; }

        [JsonProperty(PropertyName = "properties")]
        public Dictionary<string, string> Properties { get; set;}

        //public TestMessage(string ClientId, Dictionary<string, string> Properties)
        //{
        //    this.ClientId = ClientId;
        //    this.Properties = Properties;
        //}

    }
}
