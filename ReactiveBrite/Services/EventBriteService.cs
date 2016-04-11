using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ModernHttpClient;
using Newtonsoft.Json;
using ReactiveBrite.Models;


namespace ReactiveBrite.Services
{
    class EventBriteService : IEventBriteService
    {
        private const string EventBriteUrl = "http://api.tekconf.com/v1/";
        private string authorizationKey;

        private async Task<HttpClient> GetClient()
        {
            var client = new HttpClient(new NativeMessageHandler());

            if (string.IsNullOrEmpty(authorizationKey))
            {
                authorizationKey = await client.GetStringAsync(EventBriteUrl + "login");
                authorizationKey = authorizationKey.Trim('"');
            }

            client.DefaultRequestHeaders.Add("Authorization", authorizationKey);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<IEnumerable<Event>> GetEvents(string search)
        {
            var client = await GetClient();
            var result = await client.GetStringAsync(EventBriteUrl);
            return JsonConvert.DeserializeObject<IEnumerable<Event>>(result);
        }

        public Task<Event> GetEvent(string name)
        {
            throw new NotImplementedException();
        }
    }
}
