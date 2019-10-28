using Infrastructure.Contracts;
using Infrastructure.Extensions;
using NetworkService.Contracts;
using NetworkService.Model;
using System;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkService.Services
{
    public class PingReplyService : IPingReplyService, IRequestService, INetworkFacadeService
    {
        /// <summary>
        /// https://edi.wang/post/2017/11/6/use-icmp-ping-in-netcore-20
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
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

        // https://dns.google.com/resolve?name=bing.com&type=A

        /// <summary>
        /// https://dns.google.com/
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public async Task<string> QueryDns(string target)
        {
            target.ValidateUrl(out Uri result);

            return await this.SendHttpRequestAsync<string>(result);
            //using (var client = new System.Net.Http.HttpClient())
            //{
            //    var request = new System.Net.Http.HttpRequestMessage
            //    {
            //        RequestUri = new Uri(result.AbsoluteUri)
            //    };

            //    var response = await client.SendAsync(request);
            //    return await response.Content.ReadAsStringAsync();
            //}
        }
    }
}
