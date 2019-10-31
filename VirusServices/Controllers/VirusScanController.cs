using Infrastructure.Filters;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirusService.Contracts;
using VirusService.Models;

namespace VirusService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VirusScanController : ControllerBase
    {
        protected readonly IScanService _urlScanService;
        protected readonly IOptions<AppSettings> _settings;

        public VirusScanController(IOptions<AppSettings> settings, IScanService urlScanService)
        {
            _settings = settings;
            _urlScanService = urlScanService;
        }

        // GET: api/Network
        [HttpGet]
        public IEnumerable<string> GetVirusService()
        {
            return new string[] { this._settings.Value.VirusScanUrl };
        }

        // GET: api/VirusScan/target
        [HttpGet("{target}", Name = "Get")]
        [RateLimitFilter(Name = "Rate Limit", Seconds = 10, ObjectType = typeof(Scan))]
        [ValidateDomainFilter]
        public async Task<Scan> Get(string target)
        {
            Scan scan = new Scan();

            try
            {
                scan = await this._urlScanService.ScanWebsite(target);
            }
            catch (Exception ex)
            {
                scan.Message = ex.Message;
                scan.StackTrace = ex.StackTrace;
            }

            return scan;
        }
    }
}
