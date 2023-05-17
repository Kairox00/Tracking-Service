using MassTransit;
using Newtonsoft.Json;

namespace Gameball.MassTransit.DTOs.Segment
{

    
    public class SpecMessage
    {
        public string ClientId { get; set; }

        public Dictionary<string, object> Properties { get; set; }

        public SpecMessage(string ClientId, Dictionary<string, object> Properties)
        {
            this.ClientId = ClientId;
            this.Properties = Properties;
        }

    }
}
