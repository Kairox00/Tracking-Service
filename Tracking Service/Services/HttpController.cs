using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;


namespace Tracking_Service.Services
{
    public class HttpController
    {
        public static async Task<string> Get(string url)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            using (var client = new HttpClient())
            {
                
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

    }

    

}
