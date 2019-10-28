using Infrastructure.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public static class Request
    {
        public static async Task<T> SendRequestAsync<T>(Uri uri) where T : class
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(uri);

                response.EnsureSuccessStatusCode();
                T result = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());

                return result;
            }
        }
    }
}
