using Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    [Serializable]
    public class NetworkReply : IEntityService
    {            
        public string AddressFamily { get; set; }
        public string IP { get; set; }
        public string TargetHost { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public bool Status { get; set; }
        public bool IsIPv4MappedToIPv6;
        public bool IsIPv6LinkLocal;
        public bool IsIPv6Multicast;
        public bool IsIPv6SiteLocal;
        public bool IsIPv6Teredo;
    }
}
