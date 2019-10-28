using Infrastructure.Contracts;
using Infrastructure.Extensions;
using System;
using System.Threading.Tasks;
using VirusService.Contracts;
using VirusService.Models;
using VirusService.Parsers;

namespace VirusService.Services
{
    public class ScanService : IScanService, IRequestService
    {
        public async Task<Scan> ScanWebsite(string target)
        {
            target.ValidateUrl(out Uri result);

            var json = await this.SendHttpRequestAsync(result);

            return JsonScanParser.Parse(json);
        }
    }
}
