using Infrastructure.Contracts;
using Infrastructure.Extensions;
using Infrastructure.Models;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using VirusService.Contracts;
using VirusService.Models;
using VirusService.Parsers;

namespace VirusService.Services
{
    public class ScanService : IScanService, IRequestService
    {
        protected readonly IOptions<AppSettings> _settings;

        public ScanService(IOptions<AppSettings> settings)
        {
            _settings = settings;
        }

        public async Task<Scan> ScanWebsite(string target)
        {
            string targetHost = $"{this._settings.Value.VirusScanUrl}{target}";

            targetHost.ValidateUrl(out Uri result);

            var json = await this.SendHttpRequestAsync(result);

            return JsonScanParser.Parse(json);
        }
    }
}
