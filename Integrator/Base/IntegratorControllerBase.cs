using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integrator.Base
{
    public class IntegratorControllerBase : Controller
    {
        protected async Task<T> RequestClient<T>(string target) where T : class
        {
            T result = default(T);

            using (var client = new System.Net.Http.HttpClient())
            {
                var request = new System.Net.Http.HttpRequestMessage();
                request.RequestUri = new Uri(target);
                var response = await client.SendAsync(request);
                result = response.Content.ReadAsStringAsync().Result as T;
            }

            return result;
        }
    }
}
