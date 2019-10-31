using Infrastructure.Filters;
using Infrastructure.Models;
using Integrator.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace RestIntegrator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        protected readonly IConfiguration _configuration;
        protected readonly IOptions<Endpoints> _endpoints;
        protected readonly IIntegratorService _integratorService;

        public MainController(IConfiguration configuration
            , IOptions<Endpoints> endpoints
            , IIntegratorService integratorService)
        {
            _endpoints = endpoints;
            _integratorService = integratorService;
            _configuration = configuration;
        }

        // GET api/main
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    var values =  new string[2];

        //    try
        //    {
        //        values = new[]{ "value1", "value2" };
        //    }
        //    catch(Exception ex)
        //    {
        //        var test = ex.Message;
        //    }
        //    return values;
        //}

        [HttpGet]
        [Route("[action]/{target}/{items}")]
        [ValidateDomainFilter]
        public async Task<List<string>> Data(string target, string items)
        {
            string targetHost = (string.IsNullOrEmpty(target) ? _endpoints.Value.DefaultTarget.ToString() : target).Trim();

            if (IPAddress.TryParse(target, out IPAddress ipAddress))
            {
                var resolverHost = this._configuration.GetValue<string>("GetHostname");
                var getHostname = $"{resolverHost}{ipAddress}";
                var hostname = await this._integratorService.RequestClient(getHostname);
                targetHost = JsonConvert.DeserializeObject<string>(hostname);
            }

            var services = JsonConvert.DeserializeObject<List<string>>(items);

            var results = new List<string>
            {
                targetHost
            };

            results.AddRange(await this._integratorService.ExecuteServices(targetHost, services));

            return results;
        }
    }
}
