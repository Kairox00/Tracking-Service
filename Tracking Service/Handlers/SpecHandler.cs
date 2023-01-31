using Dummy_Server.Models;
using System.Text.Json;

namespace Tracking_Service.Handlers
{
    public class SpecHandler
    {
        public void MakeCall(string messageString)
        {
            SpecMessage msg = JsonSerializer.Deserialize<SpecMessage>(messageString);
            switch (msg.type)
            {
                case "Identify":
                    new IdentifyHandler().MakeCall(msg);
                    break;

                case "Track":
                    new TrackHandler().MakeCall(msg);
                    break;
            }
               
        }

        protected Dictionary<string, object> ParseDictValues(Dictionary<string, object> input, Dictionary<string, string> map)
        {
            var dict = new Dictionary<string, object>();
            foreach (var entry in input)
            {
                string valueString = entry.Value.ToString();
                Type dataType = Type.GetType(map[entry.Key]);
                dict.Add(entry.Key, Convert.ChangeType(valueString, dataType));
            }

            return dict;
        }
    }
}
