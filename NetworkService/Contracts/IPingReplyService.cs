using Infrastructure.Contracts;
using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetworkService.Contracts
{
    public interface IPingReplyService : IRequestService
    {
        Task<NetworkReply> ExecuteICMP(string target);
        Task<string> QueryDns(string target);
    }
}
