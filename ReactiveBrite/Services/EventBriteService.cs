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
        private HttpClient GetClient()
        {
            var client = new HttpClient(new NativeMessageHandler());

            //if (string.IsNullOrEmpty(authorizationKey))
            //{
            //    authorizationKey = await client.GetStringAsync(EventBriteUrl + "login");
            //    authorizationKey = authorizationKey.Trim('"');
            //}

            client.DefaultRequestHeaders.Add("Authorization", "Bearer" + EventbriteConstants.PersonalToken);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<IEnumerable<Event>> GetEvents(string search)
        {
            var client = GetClient();
            var result = await client.GetStringAsync(EventbriteConstants.EventbriteRootUrl);
            return JsonConvert.DeserializeObject<IEnumerable<Event>>(result);
        }

        public async Task<Event> GetEvent(string id)
        {
            var client = GetClient();
            var result = await client.GetStringAsync(EventbriteConstants.EventbriteRootUrl);
            return JsonConvert.DeserializeObject<Event>(result);
        }
    }
}
