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
            return db.Value[id][key];
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

        //public void SendValues(string keyString)
        //{
        //    string [] keys = JsonSerializer.Deserialize<string[]>(keyString);
        //    var vals = GetValues(keys);
        //    var options = new JsonSerializerOptions
        //    {
        //        WriteIndented = true
        //    };
        //    var jsonString = JsonSerializer.Serialize(vals, options);

        //    new Publisher().Publish("To_Service", jsonString);
        //}


    }
}
