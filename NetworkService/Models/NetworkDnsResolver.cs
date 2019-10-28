using Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetworkService.Models
{
    public class DnsResolverQuestion
    {
        public string Name { get; set; }
        public int Type { get; set; }
    }

    public class DnsResolverAnswer
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public int TTL { get; set; }
        public string Data { get; set; }
    }

    public class NetworkDnsResolver : IEntityService
    {
        public NetworkDnsResolver()
        {
            Questions = new List<DnsResolverQuestion>();
            Answers = new List<DnsResolverAnswer>();
        }

        public bool Status { get; set; }
        public bool TC { get; set; }
        public bool RD { get; set; }
        public bool RA { get; set; }
        public bool AD { get; set; }
        public bool CD { get; set; }
        public List<DnsResolverQuestion> Questions { get; set; }
        public List<DnsResolverAnswer> Answers { get; set; }

        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}
