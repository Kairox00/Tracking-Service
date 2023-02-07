using Dummy_Server.Models;
using Dummy_Server.Services;
using System.Text.Json;

namespace Dummy_Server.Database
{
    public class DB : Dictionary<string, Dictionary<string, string>>
    {
        private DB()
        {
            this.Add("123", new Dictionary<string, string>
            {
                {"company","gameball" },
                {"job","sales"},
                { "companyAddress","zamalek"},
                {"didTheyQuit?","True"},
                { "pricePlan", "premium" },
                {"cost", "10000"}
            });

            this.Add("1234", new Dictionary<string, string>
            {
                {"company","starbucks" },
                {"job","manager"},
                { "companyAddress","new cairo"},
                {"didTheyQuit?","False"},
                { "pricePlan", "basic" },
                {"cost", "100"}
            });
        }

        private static readonly Lazy<DB> db = new Lazy<DB>(() => new DB());
        public static DB Instance
        {
            get
            {
                return db.Value;
            }
        }

        public string GetValue(string id, string key)
        {
            bool keyExists = db.Value[id].TryGetValue(key, out var value);
            if (keyExists) return value;
            return "None";
        }

        public bool UserExistsInDB(string id)
        {
            return db.Value.ContainsKey(id);
        }

        //public Dictionary<string, object> GetValues(string id, List<string> keys)
        //{
        //    Dictionary<string, object> res = new();
        //    foreach (var key in keys)
        //    {
        //        var value = db.Value[id][key];
        //        res.Add(key,value);
        //    }
        //    return res;
        //}


    }
}
