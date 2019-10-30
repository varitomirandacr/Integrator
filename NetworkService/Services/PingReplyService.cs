using Infrastructure.Contracts;
using Infrastructure.Extensions;
using Infrastructure.Models;
using Microsoft.Extensions.Options;
using NetworkService.Contracts;
using NetworkService.Model;
using NetworkService.Models;
using NetworkService.Parsers;
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkService.Services
{
    public class PingReplyService : IPingReplyService, IRequestService
    {
        protected readonly IOptions<AppSettings> _settings;

        public PingReplyService(IOptions<AppSettings> settings)
        {
            _settings = settings;
        }

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
        public async Task<NetworkDnsResolver> ExecuteDnsResolver(string target)
        {
            var targetHost = $"{_settings.Value.QueryDnsUrl}{target}";

            targetHost.ValidateUrl(out Uri result);

            var response = await this.SendHttpRequestAsync(result);

            return JsonDnsResolverParser.Parse(response);
        }

        public async Task<NetworkIpResolver> ExecuteIPAddressResolver(string ipAddress)
        {
            NetworkIpResolver resolver = new NetworkIpResolver();
            resolver.IP = ipAddress;
                        
            await Task.Run(() =>
            {
                try
                {
                    // Example "54.172.75.131"
                    IPHostEntry ipToDomain = Dns.GetHostEntry(ipAddress);
                    resolver.HostName = ipToDomain.HostName;
                }
                catch (Exception ex)
                {
                    resolver.HostName = "Unknown";
                    resolver.Message = ex?.Message;
                    resolver.StackTrace = ex?.StackTrace;
                }
            });           

            return resolver;
        }
    }
}
