using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirusService.Contracts;
using VirusService.Models;

namespace VirusService.Services
{
    public class UrlScanService : IUrlScanService
    {
        public async Task<string> ScanWebsite(string target)
        {
            if (!Uri.TryCreate(target, UriKind.Absolute, out Uri result))
            {
                throw new Exception("Url has an invalid format");
            }

            using (var client = new System.Net.Http.HttpClient())
            {
                var request = new System.Net.Http.HttpRequestMessage
                {
                    RequestUri = new Uri(result.AbsoluteUri)
                };

                var response = await client.SendAsync(request);
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
