using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VirusService.Contracts;
using VirusService.Models;
using VirusService.Services;

namespace VirusService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VirusScanController : ControllerBase
    {
        private readonly IUrlScanService _urlScanService;
        protected readonly IOptions<AppSettings> _settings;

        public VirusScanController(IOptions<AppSettings> settings, IUrlScanService urlScanService)
        {
            _settings = settings;
            _urlScanService = urlScanService;
        }

        // GET: api/VirusScan
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "test", "value2", this._settings.Value.VirusScanUrl };
        }

        // GET: api/VirusScan/5
        [HttpGet("{target}", Name = "Get")]
        public string Get(string target)
        {
            string targetHost = $"{this._settings.Value.VirusScanUrl}{target}";

            (this._urlScanService ?? new UrlScanService()).ScanWebsite(targetHost);
            return "value";
        }
    }
}
