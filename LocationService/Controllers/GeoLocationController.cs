using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeGeoIPCore.Models;
using Infrastructure.Models;
using LocationService.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LocationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoLocationController : ControllerBase
    {
        protected readonly IGeoIpService _geoIpService;
        protected readonly IOptions<AppSettings> _settings;

        public GeoLocationController(IOptions<AppSettings> settings, IGeoIpService geoIpService)
        {
            _settings = settings;
            _geoIpService = geoIpService;
        }

        // GET: api/GeoLocation
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2", this._settings.Value.GeoIpUrl };
        }

        // GET: api/GeoLocation/5
        [HttpGet("{target}", Name = "Get")]
        public async Task<Location> Get(string target)
        {
            var targetHost = $"{_settings.Value.GeoIpUrl}{target}{_settings.Value.GeoIpAccessKey}";

            return await _geoIpService.GetLocation(targetHost);
        }
    }
}
