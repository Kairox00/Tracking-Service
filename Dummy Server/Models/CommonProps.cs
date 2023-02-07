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
            this.Add("11", new List<string>(new string[] { "pricePlan"}));
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

        public Dictionary<string, string> GetAll(string id, string commonIndecesString)
        {
            string[] commonIndeces = commonIndecesString.Split('&');
            Dictionary<string, Dictionary<string, string>> CachedProps;
            bool userExistsInCache = Cache.KeyExists(id);
            if (DB.Instance.UserExistsInDB(id))
            {
                try
                {
                    CachedProps = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(Cache.StringGet(id));
                }
                catch(Exception e)
                {
                    CachedProps = new();
                }
                Dictionary<string, string> res = new();
                foreach (var commonIndex in commonIndeces)
                {
                    bool commonExists = common.Value.TryGetValue(commonIndex, out List<string> props);
                    if (!commonExists)
                    {
                        return new Dictionary<string, string>() { { "error", $"index {commonIndex} not found" } };
                    }
                    Dictionary<string, string> dict = new();
                    foreach (string prop in props)
                    {
                        dict[prop] = DB.Instance.GetValue(id, prop);
                        res[prop] = DB.Instance.GetValue(id, prop);
                    }
                    CachedProps[commonIndex] = dict;
                }
                Cache.StringSet(id, JsonSerializer.Serialize(CachedProps));

                return res;
            }
            return new Dictionary<string, string>() { { "error", $"user {id} not found" } };
        }

        public Dictionary<string, string> GetOne(string id, string commonIndex)
        {
            Dictionary<string, Dictionary<string, string>> CachedProps;
            bool userExistsInCache = Cache.KeyExists(id);
            if (DB.Instance.UserExistsInDB(id))
            {
                try
                {
                    CachedProps = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(Cache.StringGet(id));
                }
                catch (Exception e)
                {
                    CachedProps = new();
                }
                bool commonExists = common.Value.TryGetValue(commonIndex, out List<string> props);
                if (!commonExists)
                {
                    return new Dictionary<string, string>() { { "error", $"index {commonIndex} not found" } };
                }
                Dictionary<string, string> res = new();
                foreach (string prop in props)
                {
                    res[prop] = DB.Instance.GetValue(id, prop);
                }
                CachedProps[commonIndex] = res;
                Cache.StringSet(id, JsonSerializer.Serialize<Dictionary<string, Dictionary<string, string>>>(CachedProps));

                return res;
            }

            return new Dictionary<string, string>() { { "error", $"user {id} not found" } };

        }

    }
}
