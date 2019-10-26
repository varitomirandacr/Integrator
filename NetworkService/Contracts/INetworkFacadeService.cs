using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace NetworkService.Contracts
{
    interface INetworkFacadeService : IIntegratorService
    {
        PingReply ExecuteICMP(string target);
    }
}
