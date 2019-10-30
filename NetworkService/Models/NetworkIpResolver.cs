using Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetworkService.Models
{
    public class NetworkIpResolver : IEntityService
    {
        public string IP { get; set; }

        public string HostName { get; set; }

        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}
