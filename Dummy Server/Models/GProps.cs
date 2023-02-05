using Dummy_Server.Cache;
using Dummy_Server.Database;
using Microsoft.AspNetCore.SignalR;
using StackExchange.Redis;
using System.Text.Json;

namespace Dummy_Server.Models
{
    public class GProps : Dictionary<string, List<string>>
    {
        IDatabase Cache;
        private GProps()
        {
            this.Add("1", new List<string>(new string[] { "company", "job" }));
            this.Add("2", new List<string>(new string[] { "companyAddress", "didTheyQuit?" }));
            this.Add("10", new List<string>(new string[] { "pricePlan", "cost" }));
            Cache = new RedisController().db;
        }
        private static readonly Lazy<GProps> gprops = new Lazy<GProps>(() => new GProps());
        public static GProps Instance
        {
            get
            {
                return gprops.Value;
            }
        }

        public Dictionary<string, string> GetAll(string id, string[] gIndeces)
        {
            Dictionary<string, Dictionary<string, string>> CachedProps = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(Cache.StringGet(id));
            Dictionary<string, string> res = new();
            foreach (var gIndex in gIndeces)
            {
                List<string> props = gprops.Value[gIndex];
                Dictionary<string, string> dict = new();
                foreach (string prop in props)
                {
                    dict.Add(prop, DB.Instance.GetValue(id, prop));
                    res.Add(prop, DB.Instance.GetValue(id, prop));
                }
                CachedProps.Add(gIndex, dict);
            }
            Cache.StringSet(id, JsonSerializer.Serialize(CachedProps));
        
            return res;
        }

        public Dictionary<string, string> GetOne(string id, string gIndex)
        {
            Dictionary<string, Dictionary<string, string>> CachedProps = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(Cache.StringGet(id));
            Dictionary<string, string> res = new();
            List<string> props = gprops.Value[gIndex];
            foreach (string prop in props)
            {
                res.Add(prop, DB.Instance.GetValue(id, prop));
            }
            CachedProps.Add(gIndex, res);
            Cache.StringSet(id, JsonSerializer.Serialize<Dictionary<string, Dictionary<string, string>>>(CachedProps));

            return res;
        }

    }
}
