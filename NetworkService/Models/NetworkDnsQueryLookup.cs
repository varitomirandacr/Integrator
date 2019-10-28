using Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetworkService.Models
{
    public class NetworkDnsQueryLookup : IEntityService
    {
        public string IpAddress { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}
