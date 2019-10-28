using Infrastructure.Contracts;
using Infrastructure.Extensions;
using System;
using System.Threading.Tasks;
using VirusService.Contracts;

namespace VirusService.Services
{
    public class ScanService : IScanService, IRequestService
    {
        public async Task<string> ScanWebsite(string target)
        {
            target.ValidateUrl(out Uri result);

            return await this.SendHttpRequestAsync<string>(result);
            //using (var client = new System.Net.Http.HttpClient())
            //{
            //    var request = new System.Net.Http.HttpRequestMessage
            //    {
            //        RequestUri = new Uri(result.AbsoluteUri)
            //    };

            //    var response = await client.SendAsync(request);
            //    return await response.Content.ReadAsStringAsync();
            //}
        }
    }
}
