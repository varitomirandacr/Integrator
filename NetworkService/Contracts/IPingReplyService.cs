using Infrastructure.Contracts;
using NetworkService.Model;
using NetworkService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetworkService.Contracts
{
    public interface IPingReplyService : IRequestService
    {
        Task<NetworkReply> ExecuteICMP(string target);
        Task<NetworkDnsResolver> ExecuteDnsResolver(string target);
        Task<NetworkIpResolver> ExecuteIPAddressResolver(string ipAddress);
    }
}
