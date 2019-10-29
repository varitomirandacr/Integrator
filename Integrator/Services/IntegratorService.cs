﻿using Infrastructure.Models;
using Integrator.Contracts;
using Integrator.Models;
using Microsoft.Extensions.Options;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace Integrator.Services
{
    public class IntegratorService : IIntegratorService
    {
        protected readonly IOptions<Endpoints> _endpoints;

        public IntegratorService(IOptions<Endpoints> endpoints)
        {
            _endpoints = endpoints;
        }

        public async Task<List<string>> ExecuteServices(string target, List<string> services)
        {
            var payload = new List<string>();

            try
            {
                await Task.Run(() => { 

                    var requests = GetProperties(target, services).ToList();

                    Parallel.ForEach(requests, serv =>
                    {
                        payload.Add(serv.ExecuteService(serv.RequestUrl).Result);
                    });
                });
            }
            catch(Exception ex)
            {
                var test = ex.Message;
                var stack = ex.StackTrace;
            }
            return payload;
        }

        private IEnumerable<IIntegratorRequest> GetProperties(string target, List<string> services)
        {
            IEnumerable<PropertyInfo> props = typeof(Endpoints).GetProperties();

            Func<PropertyInfo, bool> filterFunc = (services == null)
                ? new Func<PropertyInfo, bool>(x => x.PropertyType.Name != "Object")
                : new Func<PropertyInfo, bool>(x => services.Contains(x.Name) && x.PropertyType.Name != "Object");

            var requests = props.Where(filterFunc);

            return requests.Select(p => new IntegratorRequest
            {
                ServiceName = p.Name,
                RequestUrl = $"{this._endpoints.Value[p.Name] as string}{target}",
                ExecuteService = async (x) => { return await RequestClient(x); },
                // ExecuteAction = (x, list) => RequestClient(x, list)
            });
        }
        
        public static async Task<string> RequestClient(string target)
        {
            // Testing URLs
            // http://locationservice20191027105615.azurewebsites.net/api/GeoLocation/google.com
            // https://localhost:44313/api/network

            var request = new System.Net.Http.HttpRequestMessage(HttpMethod.Get, new Uri(target));
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var client = new HttpClient(new WinHttpHandler() { WindowsProxyUsePolicy = WindowsProxyUsePolicy.UseWinInetProxy });
            var response = await client.SendAsync(request); 
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception("An unexpected error prevented the services to be ran");
            }
        }
    }
}
