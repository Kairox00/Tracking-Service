using Dummy_Server.Models;
using StackExchange.Redis;
using System.Text.Json;
using Tracking_Service.Cache;
using Tracking_Service.Services;
using static StackExchange.Redis.Role;

namespace Tracking_Service.Handlers
{
    public class SpecHandler
    {
        RedisController _redis;
        IDatabase cache;
        public SpecHandler()
        {
            _redis = new RedisController();
            cache = _redis.db;
        }
        
        public async void MakeCall(string messageString)
        {
            SpecMessage msg = JsonSerializer.Deserialize<SpecMessage>(messageString);
            msg.properties.Remove("needG", out string needGString);
            string[] needG = needGString.Split(',');
            Dictionary<string, string> allProps = new();

            if (needG != null && needG.Length > 0)
            {
                var CachedGProps = GetCachedProps(msg.clientId);
                foreach(var gIndex in needG)
                {
                    Dictionary<string, string> GProp = new();
                    if (CachedGProps.ContainsKey(gIndex))
                    {
                        GProp = CachedGProps[gIndex];
                    }
                    else
                    {
                        var response = await HttpController.Get($"https://localhost:5001/{msg.clientId}/props/G/{gIndex}");
                        Dictionary<string, string> res = JsonSerializer.Deserialize<Dictionary<string, string>>(response);
                        GProp = res;
                    }
                    GProp.ToList().ForEach(pair => allProps[pair.Key] = pair.Value);
                }
            }

            msg.properties.Remove("type", out string type);
            allProps.ToList().ForEach(pair => msg.properties[pair.Key] = pair.Value);
            switch (type)
            {
                case "Identify":
                    new IdentifyHandler().MakeCall(msg);
                    break;

                case "Track":
                    new TrackHandler().MakeCall(msg);
                    break;

                default:
                    Console.WriteLine("Problem in type switch");
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


        private Dictionary<string, Dictionary<string, string>> GetCachedProps(string id)
        {
            if (cache.KeyExists(id))
            {
                string temp = cache.StringGet(id);
                return JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(temp);
            }
            else
            {
                cache.SetAdd(id,"{}");
                return new Dictionary<string, Dictionary<string, string>>();
            }
        }
       
    }
}
