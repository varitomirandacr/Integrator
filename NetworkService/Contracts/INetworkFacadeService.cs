using Infrastructure;
using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace NetworkService.Contracts
{
    interface INetworkFacadeService
    {
        Task<NetworkReply> ExecuteICMP(string target);
    }
}
