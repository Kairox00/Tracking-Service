using StackExchange.Redis;

namespace Tracking_Service.Cache
{
    enum ECacheHeaders
    {
        absexp = 0,
        data = 1
    }

    public class CacheService : ICacheService
    {
        private RedisServer _redisServer;

        public CacheService(RedisServer redisServer)
        {
            _redisServer = redisServer;
        }

        public string GetString(string key)
        {
            try
            {
                var value = _redisServer.Database.HashGetAll(key);
                if (value is null || value.Length == 0)
                {
                    return null;
                }

                return value.FirstOrDefault(v => v.Name == ECacheHeaders.data.ToString()).Value;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //public T Get<T>(string key)
        //{
        //    try
        //    {
        //        var result = GetString(key);
        //        if (result != null)
        //            return CustomJsonConvert.Deserialize<T>(result);
        //        else
        //            return default;
        //    }
        //    catch (Exception ex)
        //    {
        //                   return default;
        //    }
        //}

        public void Set<T>(string key, T value, int expiryInMinutes = 24 * 60)
        {
            try
            {
                string cacheValue = "";
                var expiryTime = TimeSpan.FromMinutes(expiryInMinutes);
                if (value is int || value is string)
                {
                    cacheValue = value.ToString();
                }
                //else
                //{
                //    cacheValue = CustomJsonConvert.Serialize(value);
                //}
                HashEntry[] redisHash = new HashEntry[]
                {
                   new HashEntry(ECacheHeaders.absexp.ToString(), expiryInMinutes * 60),
                   new HashEntry(ECacheHeaders.data.ToString(), cacheValue)
                };
                _redisServer.Database.HashSet(key, redisHash);
                _redisServer.Database.KeyExpire(key, expiryTime);
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public void DeleteValue(string key)
        {
            try
            {
                _redisServer.Database.KeyDelete(key);
            }
            catch (Exception ex)
            {
               return;
            }
        }

        public bool KeyExists(string key)
        {
            try
            {
                return _redisServer.Database.KeyExists(key);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
