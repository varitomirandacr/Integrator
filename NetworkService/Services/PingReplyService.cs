using NetworkService.Contracts;
using NetworkService.Model;
using System;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkService.Services
{
    public class PingReplyService : IPingReplyService, INetworkFacadeService
    {
        public NetworkReply Reply { get; set; }

        public async Task<NetworkReply> ExecuteICMP(string target)
        {
            using (Ping pingSender = new Ping())
            {
                // Create a buffer of 32 bytes of data to be transmitted.
                string data = "";
                byte[] buffer = Encoding.ASCII.GetBytes(data);

                // Wait 12 seconds for a reply.
                int timeout = 12000;

                PingOptions options = new PingOptions(64, true);
              
                NetworkReply network = new NetworkReply();
                
                PingReply reply = await pingSender.SendPingAsync(target, timeout, buffer, options);                

                var address = reply.Address;
                network.AddressFamily = address != null ? address.AddressFamily.ToString() : string.Empty;
                network.IP = address != null ? address.ToString() : string.Empty;
                network.IsIPv4MappedToIPv6 = address != null ? address.IsIPv4MappedToIPv6 : false;
                network.IsIPv6Multicast = address != null ? address.IsIPv6Multicast : false;
                network.IsIPv6LinkLocal = address != null ? address.IsIPv6LinkLocal : false;
                network.IsIPv6SiteLocal = address != null ? address.IsIPv6SiteLocal : false;
                network.IsIPv6Teredo = address != null ? address.IsIPv6SiteLocal : false;
                network.TargetHost = target;
                network.Status = reply.Status == IPStatus.Success;
                
                return network;
            }
        }
    }
}
