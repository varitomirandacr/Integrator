using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetworkService.Contracts
{
    public interface IPingReplyService
    {
        Task<NetworkReply> ExecuteICMP(string target);
    }
}
