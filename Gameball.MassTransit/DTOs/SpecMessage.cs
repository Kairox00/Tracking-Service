namespace Gameball.MassTransit
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

    }
}
