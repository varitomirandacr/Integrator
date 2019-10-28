using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetworkService.Contracts;
using NetworkService.Model;
using NetworkService.Models;
using NetworkService.Services;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace NetworkService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetworkController : ControllerBase
    {
        protected readonly IDnsLookupService _dnsLookupService;
        protected readonly IPingReplyService _pingReplyService;
        protected readonly IOptions<AppSettings> _settings;

        public NetworkController(IOptions<AppSettings> settings, IDnsLookupService dnsLookupService, IPingReplyService pingReplyService)
        {
            _settings = settings;
            _dnsLookupService = dnsLookupService;
            _pingReplyService = pingReplyService;
        }

        // GET: api/Network
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Network/icmp/target
        [HttpGet]
        [Route("[action]/{target}")]
        public async Task<NetworkReply> Icmp(string target)
        {
            NetworkReply reply = new NetworkReply();

            try
            {
                reply = await this._pingReplyService.ExecuteICMP(target);
            }
            catch (PingException pex)
            {
                reply.Message = pex.Message;
                reply.StackTrace = pex.StackTrace;
            }
            catch (Exception ex)
            {
                reply.Message = ex.InnerException.Message;
                reply.StackTrace = ex.InnerException.StackTrace;
            }

            return reply;
        }

        // GET: api/Network/dnsresolver/target
        [HttpGet]
        [Route("[action]/{target}")]
        public async Task<NetworkDnsResolver> DnsResolver(string target)
        {
            NetworkDnsResolver resolver = new NetworkDnsResolver();

            try
            {
                resolver = await this._pingReplyService.ExecuteDnsResolver(target);
            }
            catch (Exception ex)
            {
                resolver.Message = ex.Message;
                resolver.StackTrace = ex.StackTrace;
            }

            return resolver;
        }

        // GET: api/Network/dnschilkatlookup/target
        [HttpGet]
        [Route("[action]/{target}")]
        public async Task<NetworkDnsQueryLookup> DnsChilkatLookup(string target)
        {
            NetworkDnsQueryLookup lookup = new NetworkDnsQueryLookup();

            try
            {
                lookup = await this._dnsLookupService.DnsChilkatLookup(target);
            }
            catch (Exception ex)
            {
                lookup.Message = ex.Message;
                lookup.StackTrace = ex.StackTrace;
            }

            return lookup;
        }

        // GET: api/Network/dnsclientlookup/target
        [HttpGet]
        [Route("[action]/{target}")]
        public async Task<NetworkDnsClientLookup> DnsClientLookup(string target)
        {
            NetworkDnsClientLookup lookup = new NetworkDnsClientLookup();

            try
            {
                lookup = await this._dnsLookupService.DnsClientLookup(target);
            }
            catch (Exception ex)
            {
                lookup.Message = ex.Message;
                lookup.StackTrace = ex.StackTrace;
            }

            return lookup;
        }        
    }
}
