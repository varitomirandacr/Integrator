using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integrator.Base
{
    public class IntegratorControllerBase : Controller
    {
        protected async Task<string> RequestClient(string target)
        {
            string result;
            using (var client = new System.Net.Http.HttpClient())
            {
                var request = new System.Net.Http.HttpRequestMessage
                {
                    RequestUri = new Uri(target)
                };

                var response = await client.SendAsync(request);
                result = await response.Content.ReadAsStringAsync();
            }

            return result;
        }
    }
}
