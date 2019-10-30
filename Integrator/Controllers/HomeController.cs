using Infrastructure.Models;
using Integrator.Base;
using Integrator.Contracts;
using Integrator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace Integrator.Controllers
{
    public class HomeController : Controller
    {
        protected readonly IConfiguration _configuration;
        protected readonly IOptions<Endpoints> _endpoints;
        protected readonly IIntegratorService _integratorService;

        public HomeController(IConfiguration configuration, IOptions<Endpoints> endpoints, IIntegratorService integratorService)
        {
            _endpoints = endpoints;
            _integratorService = integratorService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
             return View();
        }

        [HttpGet]
        public async Task<JsonResult> Data(string value, string items)
        
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

            return Json(results);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
