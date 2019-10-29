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
        private readonly IHttpClientFactory _clientFactory;

        public HomeController(IOptions<Endpoints> endpoints, IIntegratorService integratorService, IHttpClientFactory clientFactory)
        {
            _endpoints = endpoints;
            _integratorService = integratorService;
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            await Task.Run(() => { return true; });

            string targetHost = "google.com";
            
            //var request = new System.Net.Http.HttpRequestMessage(HttpMethod.Get, new Uri("http://locationservice20191027105615.azurewebsites.net/api/GeoLocation/google.com"));
            ////var request = new System.Net.Http.HttpRequestMessage(HttpMethod.Get, new Uri("https://localhost:44313/api/network"));
            //request.Headers.Add("Accept", "application/json");
            //request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            ////var client = _clientFactory.CreateClient();

            //var client = new HttpClient(new WinHttpHandler() { WindowsProxyUsePolicy = WindowsProxyUsePolicy.UseWinInetProxy });
            //var response = await client.SendAsync(request); // request.RequestUri.AbsoluteUri);
            //if (response.IsSuccessStatusCode)
            //{
            //    var test = await response.Content.ReadAsStringAsync();
            //}
            //else
            //{
            //    //
            //    var test2 = "";
            //}

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
