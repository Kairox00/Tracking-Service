namespace Tracking_Service.Cache
{
    public interface ICacheService
    {
        //T Get<T>(string key);
        string GetString(string key);
        void Set<T>(string key, T value, int expiryInMinutes = 60 * 24);
        void DeleteValue(string key);
        bool KeyExists(string key);
    }
}
