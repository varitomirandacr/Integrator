using Infrastructure.Contracts;
using NetworkService.Model;
using NetworkService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetworkService.Contracts
{
    public interface IDnsLookupService : IRequestService
    {
        Task<NetworkDnsClientLookup> DnsClientLookup(string target);
        Task<NetworkDnsQueryLookup> DnsChilkatLookup(string target);
    }
}
