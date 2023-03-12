using System.Collections.Generic;
using System.Text.Json;

namespace Dummy_Server.Models
{
    public class SpecMessage
    {
        public string clientId { get; set; }

        public Dictionary<string, string> properties { get; set;}

        public SpecMessage(string clientId, Dictionary<string, string> properties)
        {
            this.clientId = clientId;
            this.properties = properties;
        }

        public string Serialize()
        {
            var jsonString = JsonSerializer.Serialize(this);
            return jsonString;
        }

    }
}
