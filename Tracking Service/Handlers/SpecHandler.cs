using Gameball.MassTransit;
using Segment.Model;
using StackExchange.Redis;
using System.Collections;
using System.Text.Json;
using Tracking_Service.Cache;
using Tracking_Service.Services;
using static StackExchange.Redis.Role;

namespace Tracking_Service.Handlers
{
    public abstract class SpecHandler
    {
        RedisController _redis;
        IDatabase cache;
        public SpecHandler()
        {
            _redis = new RedisController();
            cache = _redis.db;
        }

        /// <summary>
        /// Performs some common processing to the <see cref="SpecMessage"/> message and prepares it to be sent to the tracking service
        /// </summary>
        /// <param name="msg"></param>
        /// <returns>A <see cref="Task"/> that results in a <see cref="Dictionary{TKey, TValue}"/></returns>
        protected async Task<Dictionary<string, object>> ProcessMessage(SpecMessage msg)
        {
            //await AddCommonPropsToMessage(msg);
            //Options options = AddErrorToContext(msg);
            msg.Properties.Remove("event", out string @event);
            msg.Properties.Remove("newId", out string newId);
            msg.Properties.Remove("groupId", out string groupId);
            Dictionary<string, object> args = msg.Properties.ToDictionary(pair => pair.Key, pair => (object)pair.Value);

            return new Dictionary<string, object>()
            {
                {"clientId", msg.ClientId },
                {"event", @event },
                {"newId", newId },
                {"groupId", groupId },
                //{"options", options},
                {"args", args }
            };
        }

        /// <summary>
        /// Checks whether the id exists and of type <see cref="string"/>
        /// </summary>
        /// <param name="data"></param>
        /// <returns>True if id exists and is of type <see cref="string"/>, otherwise returns False.</returns>
        protected bool Validate(Dictionary<string, object> data)
        {
            return data["clientId"] != null && data["clientId"].GetType() == typeof(string);
        }

        /// <summary>
        /// Fetches and Inserts the Common Properties into <paramref name="msg"/>.
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private async Task AddCommonPropsToMessage(SpecMessage msg)
        {
            msg.Properties.Remove("needCommon", out string needCommonString);  
            if(needCommonString != null)
            {
                string[] needCommon = needCommonString.Split(',');
                Dictionary<string, string> allProps = new();

                if (needCommon != null && needCommon.Length > 0)
                {
                    await GetCommonProps(needCommon, msg, allProps);
                }

                allProps.ToList().ForEach(pair => msg.Properties[pair.Key] = pair.Value);
            }
               
        }

        /// <summary>
        /// Retrieves the cached properties of user with userId=<paramref name="id"/> .
        /// </summary>
        /// <param name="id">Id of the user in the cache.</param>
        /// <returns>A <see cref="Dictionary{TKey, TValue}"/> of the cached properties.</returns>
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

        /// <summary>
        /// Performs a GET request to the API and fetches the CommonProps.
        /// </summary>
        /// <param name="needCommon"> A string array of the indexes of the CommonProps required</param>
        /// <param name="msg">The message to be sent to the tracking service</param>
        /// <param name="allProps">hi</param>
        /// <returns><see cref="Task"/></returns>
        private async Task GetCommonProps(string[] needCommon, SpecMessage msg, Dictionary<string, string> allProps)
        {
            var CachedGProps = GetCachedProps(msg.ClientId);
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
                var response = await HttpController.Get($"https://localhost:7211/{msg.ClientId}/propsAll/{keysToFetch}");
                Dictionary<string, string> GProp = JsonSerializer.Deserialize<Dictionary<string, string>>(response);
                GProp.ToList().ForEach(pair => allProps[pair.Key] = pair.Value);
            }
        }

        /// <summary>
        /// Removes the error message if it exists from the <see cref="SpecMessage"/> message and adds it to the <see cref="Context"/>
        /// </summary>
        /// <param name="msg">The <see cref="SpecMessage"/> to be sent to the tracking service</param>
        /// <returns><see cref="Options"/> object containing the error message in its <see cref="Context"/></returns>
        private Options AddErrorToContext(SpecMessage msg)
        {
            msg.Properties.Remove("error", out string errorMsg);
            Options options = new Options();
            if (errorMsg != null)
            {
                Context context = new Context();
                context = context.Add("error", errorMsg);
                options.SetContext(context);
            }
            return options;
        }
       
    }
}
