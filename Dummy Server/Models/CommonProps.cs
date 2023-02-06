using Dummy_Server.Cache;
using Dummy_Server.Database;
using Microsoft.AspNetCore.SignalR;
using StackExchange.Redis;
using System.Text.Json;

namespace Dummy_Server.Models
{
    public class CommonProps : Dictionary<string, List<string>>
    {
        IDatabase Cache;
        private CommonProps()
        {
            this.Add("1", new List<string>(new string[] { "company", "job" }));
            this.Add("2", new List<string>(new string[] { "companyAddress", "didTheyQuit?" }));
            this.Add("10", new List<string>(new string[] { "pricePlan", "cost" }));
            Cache = new RedisController().db;
        }
        private static readonly Lazy<CommonProps> common = new Lazy<CommonProps>(() => new CommonProps());
        public static CommonProps Instance
        {
            get
            {
                return common.Value;
            }
        }

        public Dictionary<string, string> GetAll(string id, string[] commonIndeces)
        {
            Dictionary<string, Dictionary<string, string>> CachedProps = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(Cache.StringGet(id));
            Dictionary<string, string> res = new();
            foreach (var commonIndex in commonIndeces)
            {
                List<string> props = common.Value[commonIndex];
                Dictionary<string, string> dict = new();
                foreach (string prop in props)
                {
                    dict.Add(prop, DB.Instance.GetValue(id, prop));
                    res.Add(prop, DB.Instance.GetValue(id, prop));
                }
                CachedProps[commonIndex] = dict;
            }
            Cache.StringSet(id, JsonSerializer.Serialize(CachedProps));
        
            return res;
        }

        public Dictionary<string, string> GetOne(string id, string commonIndex)
        {
            Dictionary<string, Dictionary<string, string>> CachedProps = new();
            bool userExists = Cache.KeyExists(id);
            if (userExists)
            {
                CachedProps = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(Cache.StringGet(id));
            }
            bool exists = common.Value.TryGetValue(commonIndex, out List<string> props);
            if (!exists)
            {
                return new Dictionary<string, string>() { { "error", $"index {commonIndex} not found" } };
            }
            Dictionary<string, string> res = new();
            foreach (string prop in props)
            {
                res.Add(prop, DB.Instance.GetValue(id, prop));
            }
            CachedProps[commonIndex] = res;
            Cache.StringSet(id, JsonSerializer.Serialize<Dictionary<string, Dictionary<string, string>>>(CachedProps));

            return res;
        }

    }
}
