using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Integrator.Models;
using Integrator.Base;
using System.Net.NetworkInformation;
using Infrastructure;
using NS = NetworkService.Services;
using Integrator.Contracts;
using Microsoft.Extensions.Options;
using Infrastructure.Models;
using System.Net.Http;

namespace Integrator.Controllers
{
    public class HomeController : IntegratorControllerBase
    {
        protected readonly IIntegratorService _integratorService;
        protected readonly IOptions<Endpoints> _endpoints;

        public HomeController(IOptions<Endpoints> endpoints, IIntegratorService integratorService)
        {
            _endpoints = endpoints;
            _integratorService = integratorService;
        }

        public async Task<IActionResult> Index()
        {
            await Task.Run(() => { return true; });

            string targetHost = "google.com";
            
            var result = await this._integratorService.ExecuteServices(targetHost, null);

            return View();
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
