using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Infrastructure.Models;
using Integrator.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace RestIntegrator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        protected readonly IConfiguration _configuration;
        protected readonly IOptions<Endpoints> _endpoints;
        protected readonly IIntegratorService _integratorService;

        public MainController(IConfiguration configuration, IOptions<Endpoints> endpoints, IIntegratorService integratorService)
        {
            _endpoints = endpoints;
            _integratorService = integratorService;
            _configuration = configuration;
        }

        // GET api/main
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("[action]/{value}/{items}")]
        public async Task<List<string>> Data(string value, string items)
        {
            string target = (string.IsNullOrEmpty(value) ? _endpoints.Value.DefaultTarget.ToString() : value).Trim();

            if (IPAddress.TryParse(value, out IPAddress ipAddress))
            {
                var resolverHost = this._configuration.GetValue<string>("GetHostname");
                var getHostname = $"{resolverHost}{ipAddress}";
                var hostname = await this._integratorService.RequestClient(getHostname);
                target = JsonConvert.DeserializeObject<string>(hostname);
            }

            var services = JsonConvert.DeserializeObject<List<string>>(items);

            var results = new List<string>
            {
                target
            };

            results.AddRange(await this._integratorService.ExecuteServices(target, services));

            return results;
        }

    }
}
