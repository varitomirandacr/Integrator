using Microsoft.AspNetCore.Mvc;
using NetworkService.Contracts;
using NetworkService.Model;
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

        private readonly IDnsLookupService _dnsLookupService;
        private readonly IPingReplyService _pingReplyService;

        public NetworkController(IDnsLookupService dnsLookupService, IPingReplyService pingReplyService)
        {
            _dnsLookupService = dnsLookupService;
            _pingReplyService = pingReplyService;
        }

        // GET: api/Network
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Network/something
        [HttpGet("{target}", Name = "Get")]
        public async Task<NetworkReply> Get(string target)
        {
            NetworkReply reply = new NetworkReply();

            try
            {
                reply = await (this._pingReplyService ?? new PingReplyService()).ExecuteICMP(target);
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

        // GET: api/Network/dns/target
        [HttpGet]
        [Route("[action]/{target}")]
        public async Task<string> DnsLookup(string target)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return this._dnsLookupService.DnsLookup(target);
                });
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpGet]
        [Route("[action]/{target}")]
        public async Task<NetworkLookup> DnsClient(string target)
        {
            NetworkLookup lookup = new NetworkLookup();

            try
            {
                lookup = await Task.Run(() =>
                {
                    return this._dnsLookupService.DnsClientLookup(target);
                });
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
