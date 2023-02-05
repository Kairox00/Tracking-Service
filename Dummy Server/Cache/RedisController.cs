using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace Dummy_Server.Cache
{

    public class RedisController
    {
        private ConnectionMultiplexer connection;
        public IDatabase db;

        public RedisController()
        {
            ConnectToRedis();
        }

        public void ConnectToRedis()
        {
            connection = ConnectionMultiplexer.Connect("localhost");
            db = connection.GetDatabase();
            Console.WriteLine("Redis Connected");
        }

        public string GetValue(string key)
        {
            string value = db.StringGet(key);
            return value;
        }

        //public static void Run()
        //{
        //    string name = RedisController.GetValue("name");
        //    Console.WriteLine(name);
        //}
    }

   
}
