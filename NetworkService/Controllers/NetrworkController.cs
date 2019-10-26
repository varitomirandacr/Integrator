using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NS = NetworkService.Services;

namespace NetworkService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetrworkController : ControllerBase
    {
        // GET: api/Netrwork
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Netrwork/something
        [HttpGet("{target}", Name = "Get")]
        public async Task<PingReply> Get(string target)
        {
            NS.NetworkService service = new NS.NetworkService();
            
            PingReply reply = await Task.Run(() =>
            {
                return service.ExecuteICMP(target);
            });

            return reply;
        }
    }
}
