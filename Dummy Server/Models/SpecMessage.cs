using System.Collections.Generic;
using System.Text.Json;

namespace Dummy_Server.Models
{
    public class SpecMessage
    {
        public string type { get; set; }
        public SpecType specType { get; set; }
        public string userId { get; set; }
        public Dictionary<string, object> properties { get; set; }
        public Dictionary<string, object> traits { get; set; }
        public string @event { get; set; }
        public Dictionary<string, string> DataTypes { get; set; }



        public SpecMessage(string type, string userId, Dictionary<string, object>? properties = null, Dictionary<string, object>? traits = null, string @event = null)
        {
            this.type = type;
            this.userId = userId;
            this.properties = properties;
            this.traits = traits;
            this.@event = @event;
            DataTypes = GetTypeMap();
        }

        //public SpecMessage(SpecType type, string userId, Dictionary<string, object>? properties = null, Dictionary<string, object>? traits = null, string @event = null)
        //{
        //    this.specType = type;
        //    this.type = this.specType.ToString();
        //    this.userId = userId;
        //    this.properties = properties;
        //    this.traits = traits;
        //    this.@event = @event;
        //    DataTypes = GetTypeMap();
        //}

        Dictionary<string, string> GetTypeMap()
        {
            Dictionary<string, string> TypeMap = new();

            if (properties != null)
            {
                foreach (KeyValuePair<string, object> kvp in properties)
                {
                    TypeMap.Add(kvp.Key, kvp.Value.GetType().ToString());
                }
            }

            if (traits != null)
            {
                foreach (KeyValuePair<string, object> kvp in traits)
                {
                    TypeMap.Add(kvp.Key, kvp.Value.GetType().ToString());
                }
            }

            return TypeMap;

        }


        public string Serialize()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var jsonString = JsonSerializer.Serialize(this, options);
            return jsonString;
        }





    }
}
