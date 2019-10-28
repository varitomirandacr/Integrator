using Infrastructure.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class RequestServiceExtentions
    {
        public static async Task<T> SendGetRequestAsync<T>(this IRequestService service, Uri uri) where T : class
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(uri);

                response.EnsureSuccessStatusCode();

                return await DeserializeHttpContent<T>(response);
            }
        }

        public static async Task<string> SendHttpRequestAsync(this IRequestService service, Uri uri)
        {
            using (HttpClient client = new HttpClient())
            {
                var request = new System.Net.Http.HttpRequestMessage
                {
                    RequestUri = new Uri(uri.AbsoluteUri)
                };

                var response = await client.SendAsync(request);

                return await response.Content.ReadAsStringAsync();
            }
        }

        private static async Task<T> DeserializeHttpContent<T>(HttpResponseMessage response) where T : class
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
