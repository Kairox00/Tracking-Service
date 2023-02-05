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

        //Dictionary<string, string> GetTypeMap()
        //{
        //    Dictionary<string, string> TypeMap = new();

        //    if (properties != null)
        //    {
        //        foreach (KeyValuePair<string, object> kvp in properties)
        //        {
        //            TypeMap.Add(kvp.Key, kvp.Value.GetType().ToString());
        //        }
        //    }

        //    if (traits != null)
        //    {
        //        foreach (KeyValuePair<string, object> kvp in traits)
        //        {
        //            TypeMap.Add(kvp.Key, kvp.Value.GetType().ToString());
        //        }
        //    }

        //    return TypeMap;

        //}


        public string Serialize()
        {
            //var options = new JsonSerializerOptions
            //{
            //    WriteIndented = true
            //};
            var jsonString = JsonSerializer.Serialize(this);
            return jsonString;
        }





    }
}
