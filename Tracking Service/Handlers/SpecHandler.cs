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
            msg.properties.Remove("type", out string type);
            msg.properties.Remove("needCommon", out string needCommonString);          
            string[] needCommon = needCommonString.Split(',');
            Dictionary<string, string> allProps = new();

            if (needCommon != null && needCommon.Length > 0)
            {
                await GetCommonProps(needCommon, msg, allProps);
                //var CachedGProps = GetCachedProps(msg.clientId);
                //string keysToFetch = "";
                //foreach(var commonKey in needCommon)
                //{
                //    Dictionary<string, string> GProp = new();
                //    if (CachedGProps.ContainsKey(commonKey))
                //    {
                //        GProp = CachedGProps[commonKey];
                //        GProp.ToList().ForEach(pair => allProps[pair.Key] = pair.Value);
                //    }
                //    else
                //    {
                //        keysToFetch += commonKey + "&";
                //    }
                  
                //}
                //if (keysToFetch.Length > 0)
                //{
                //    keysToFetch = keysToFetch.Remove(keysToFetch.Length - 1);
                //    var response = await HttpController.Get($"https://localhost:5001/{msg.clientId}/propsAll/{keysToFetch}");
                //    Dictionary<string, string> GProp = JsonSerializer.Deserialize<Dictionary<string, string>>(response);
                //    GProp.ToList().ForEach(pair => allProps[pair.Key] = pair.Value);
                //}

            }

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
                return new Dictionary<string, Dictionary<string, string>>();
            }
        }

        private async Task GetCommonProps(string[] needCommon, SpecMessage msg, Dictionary<string, string> allProps)
        {
            var CachedGProps = GetCachedProps(msg.clientId);
            string keysToFetch = "";
            foreach (var commonKey in needCommon)
            {
                Dictionary<string, string> GProp = new();
                if (CachedGProps.ContainsKey(commonKey))
                {
                    GProp = CachedGProps[commonKey];
                    GProp.ToList().ForEach(pair => allProps[pair.Key] = pair.Value);
                }
                else
                {
                    keysToFetch += commonKey + "&";
                }

            }
            if (keysToFetch.Length > 0)
            {
                keysToFetch = keysToFetch.Remove(keysToFetch.Length - 1);
                var response = await HttpController.Get($"https://localhost:5001/{msg.clientId}/propsAll/{keysToFetch}");
                Dictionary<string, string> GProp = JsonSerializer.Deserialize<Dictionary<string, string>>(response);
                GProp.ToList().ForEach(pair => allProps[pair.Key] = pair.Value);
            }
        }
       
    }
}
