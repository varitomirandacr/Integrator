using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2", this._settings.Value.VirusScanUrl };
        }

        // GET: api/VirusScan/5
        [HttpGet("{target}", Name = "Get")]
        public async Task<Scan> Get(string target)
        {
            string targetHost = $"{this._settings.Value.VirusScanUrl}{target}";

            return await this._urlScanService.ScanWebsite(targetHost);
        }
    }
}
