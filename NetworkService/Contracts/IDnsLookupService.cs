using Infrastructure.Contracts;
using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetworkService.Contracts
{
    public interface IDnsLookupService : IRequestService
    {
        Task<NetworkLookup> DnsClientLookup(string target);
        string DnsLookup(string target);
    }
}
