﻿using System;
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

namespace Integrator.Controllers
{
    public class HomeController : IntegratorControllerBase
    {
        public async Task<IActionResult> Index()
        {
            List<IIntegratorService> services = new List<IIntegratorService>();
            services.Add(new NS.NetworkService());
            var result = await RequestClient<PingReply>("http://networkservice/api/network/google.com");

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
