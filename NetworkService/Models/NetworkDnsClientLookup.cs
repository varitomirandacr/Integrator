using Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    [Serializable]
    public class NetworkDnsClientLookup : IEntityService
    {
        public string DomainName { get; set; }
        public string IpAddress { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}
